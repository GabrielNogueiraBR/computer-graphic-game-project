using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLMaps : MonoBehaviour
{
    public Material mat;
    public Vector2 sb;
    float velo = 0.01f;
    float direcao = 1;
    public bool horizontal = true;
    private float proportion = 0.1f;
    public float personagemJogoX = 0;
    public float personagemJogoY = 0;

    float larguraParede = 0.5f;
    float larguraCaminho = 2.5f;

    public void Start() {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
               
    }


    public void Update() {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = true;
            personagemJogoX -= velo;
            direcao = -1;
            transform.position -= new Vector3(velo,0,0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = true;
            personagemJogoX += velo;
            direcao = 1;
            transform.position += new Vector3(velo, 0, 0);
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {
            horizontal = false;
            personagemJogoY += velo;
            direcao = 1;
            transform.position += new Vector3(0, velo, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            horizontal = false;
            personagemJogoY -= velo;
            direcao = -1;
            transform.position -= new Vector3(0, velo, 0);
        }
    }

    private void OnPostRender() {

        CreateMap();

        if (horizontal)
            PersonagemJogo_Horizontal();
        else
            PersonagemJogo_Vertical();        
    }

    void CreateMap()
    {
        CreateBorderMap();

    }

    void CreateBorderMap()
    {
        BarDown();
        BarRight();
        BarLeft();
        BarTop();

    }


    void BarDown()
    {

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(-21, 0, 0);
        GL.Vertex3(-21, larguraParede, 0);
        GL.Vertex3(0 - larguraCaminho, larguraParede, 0);
        GL.Vertex3(0 - larguraCaminho, 0, 0);        
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(-larguraCaminho - larguraParede, larguraParede, 0);
        GL.Vertex3(-larguraCaminho , larguraParede, 0);
        GL.Vertex3(-larguraCaminho , larguraParede + 7, 0);
        GL.Vertex3(-larguraCaminho - larguraParede, larguraParede + 7, 0);
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));       
        GL.Vertex3(-larguraCaminho - larguraParede, larguraParede + 7, 0);
        GL.Vertex3(-larguraCaminho - larguraParede - 7, larguraParede + 7, 0);
        GL.Vertex3(-larguraCaminho - larguraParede - 7, 7, 0);
        GL.Vertex3(-larguraCaminho - larguraParede , 7, 0);
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(0 + larguraCaminho, larguraParede, 0);
        GL.Vertex3(0 + larguraCaminho + larguraParede, larguraParede, 0);
        GL.Vertex3(0 + larguraCaminho + larguraParede, larguraParede + 14, 0);
        GL.Vertex3(0 + larguraCaminho, larguraParede + 14, 0);        
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(14, 0, 0);
        GL.Vertex3(14, larguraParede, 0);
        GL.Vertex3(0 + larguraCaminho, larguraParede, 0);
        GL.Vertex3(0 + larguraCaminho, 0, 0);
        EndGLPopMatrix();
    }
     
    void BarRight()
    {
        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(14, larguraParede, 0);
        GL.Vertex3(14, larguraParede + 35, 0);
        GL.Vertex3(14 - larguraParede, larguraParede + 35, 0);
        GL.Vertex3(14 - larguraParede, larguraParede, 0);
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(14 - larguraParede, larguraParede + 27, 0);
        GL.Vertex3(14 - larguraParede - 7, larguraParede + 27, 0);
        GL.Vertex3(14 - larguraParede - 7, larguraParede + 27 + larguraParede, 0);
        GL.Vertex3(14 - larguraParede, larguraParede + 27 + larguraParede, 0);
        EndGLPopMatrix();
    }

    void BarLeft()
    {
        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(-21, larguraParede, 0);
        GL.Vertex3(-21 + larguraParede, larguraParede, 0);
        GL.Vertex3(-21 + larguraParede, larguraParede + 35, 0);
        GL.Vertex3(-21, larguraParede + 35, 0);
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(-21 + larguraParede, larguraParede + 13, 0);
        GL.Vertex3(-21 + larguraParede, larguraParede + 13 + larguraParede, 0);
        GL.Vertex3(-21 + larguraParede + 11, larguraParede + 13 + larguraParede, 0);
        GL.Vertex3(-21 + larguraParede + 11, larguraParede + 13, 0);
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(-21 + larguraParede + 5, larguraParede + 13, 0);
        GL.Vertex3(-21 + larguraParede + 5, larguraParede + 13 - 6 - larguraParede, 0);
        GL.Vertex3(-21 + larguraParede + 5 + larguraParede, larguraParede + 13 - 6 - larguraParede, 0);
        GL.Vertex3(-21 + larguraParede + 5 + larguraParede, larguraParede + 13, 0);
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(-21 + 11, larguraParede + 13 + larguraParede, 0);
        GL.Vertex3(-21 + 11 + larguraParede, larguraParede + 13 + larguraParede, 0);
        GL.Vertex3(-21 + 11 + larguraParede, larguraParede + 13 + larguraParede + 7, 0);
        GL.Vertex3(-21 + 11, larguraParede + 13 + larguraParede + 7, 0);
        EndGLPopMatrix();

    }

    void BarTop()
    {
        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(-21 + larguraParede, 35, 0);
        GL.Vertex3(-21 + larguraParede, 35 + larguraParede, 0);
        GL.Vertex3(-21 + larguraParede + 14 - larguraCaminho, 35 + larguraParede, 0);
        GL.Vertex3(-21 + larguraParede + 14 - larguraCaminho, 35, 0);
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));        
        GL.Vertex3(14 - larguraParede, 35, 0);
        GL.Vertex3(14 - larguraParede, 35 + larguraParede, 0);
        GL.Vertex3(14 - larguraParede -20 + larguraCaminho, 35 + larguraParede, 0);
        GL.Vertex3(14 - larguraParede -20 + larguraCaminho, 35, 0);        
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(14 - larguraParede - 13, 35, 0);
        GL.Vertex3(14 - larguraParede - 13 - larguraParede, 35, 0);
        GL.Vertex3(14 - larguraParede - 13 - larguraParede, 35 - 14, 0);
        GL.Vertex3(14 - larguraParede - 13, 35 - 14, 0);
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));       
        GL.Vertex3(14 - larguraParede - 13 - larguraParede, 35 - 14, 0);
        GL.Vertex3(14 - larguraParede - 13 - larguraParede - 5, 35 - 14, 0);
        GL.Vertex3(14 - larguraParede - 13 - larguraParede - 5, 35 - 14 + larguraParede, 0);
        GL.Vertex3(14 - larguraParede - 13 - larguraParede, 35 - 14 + larguraParede, 0);        
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));        
        GL.Vertex3(14 - larguraParede - 13 - 5, 35 - 14, 0);
        GL.Vertex3(14 - larguraParede - 13 - 5 - larguraParede, 35 - 14, 0);
        GL.Vertex3(14 - larguraParede - 13 - 5 - larguraParede, 35 - 14 - 7, 0);
        GL.Vertex3(14 - larguraParede - 13 - 5, 35 - 14 - 7, 0);                
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(14 - larguraParede - 13 - 5, 35 - 14, 0);
        GL.Vertex3(14 - larguraParede - 13 - 5 - larguraParede, 35 - 14, 0);
        GL.Vertex3(14 - larguraParede - 13 - 5 - larguraParede, 35 - 14 + 7, 0);
        GL.Vertex3(14 - larguraParede - 13 - 5, 35 - 14 + 7, 0);
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(14 - larguraParede - 13 - 5 - larguraParede, 35 - 14 + 7, 0);
        GL.Vertex3(14 - larguraParede - 13 - 5 - larguraParede - 10, 35 - 14 + 7, 0);
        GL.Vertex3(14 - larguraParede - 13 - 5 - larguraParede - 10, 35 - 14 + 7 - larguraParede, 0);
        GL.Vertex3(14 - larguraParede - 13 - 5 - larguraParede, 35 - 14 + 7 - larguraParede, 0);        
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(14 - larguraParede - 13 - 5 - larguraParede - 10, 35 - 14 + 7 - larguraParede, 0);
        GL.Vertex3(14 - larguraParede - 13 - 5 - 10, 35 - 14 + 7 - larguraParede, 0);
        GL.Vertex3(14 - larguraParede - 13 - 5 - 10, 35 - 14 + 7 - larguraParede -6, 0);
        GL.Vertex3(14 - larguraParede - 13 - 5 - larguraParede - 10, 35 - 14 + 7 - larguraParede -6, 0);        
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        GL.Vertex3(14 - larguraParede - 13, 35 - 14, 0);
        GL.Vertex3(14 - larguraParede - 13 + 8, 35 - 14, 0);
        GL.Vertex3(14 - larguraParede - 13 + 8, 35 - 14 + larguraParede, 0);
        GL.Vertex3(14 - larguraParede - 13, 35 - 14 + larguraParede, 0);        
        EndGLPopMatrix();

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));        
        GL.Vertex3(14 - larguraParede - 13 + 8, 35 - 14, 0);
        GL.Vertex3(14 - larguraParede - 13 + 8 - larguraParede, 35 - 14, 0);
        GL.Vertex3(14 - larguraParede - 13 + 8 - larguraParede, 35 - 14 - 12, 0);
        GL.Vertex3(14 - larguraParede - 13 + 8, 35 - 14 - 12, 0);        
        EndGLPopMatrix();


    }

    private void PersonagemJogo_Vertical()
    {

        StartGL_Quads();

        #region CHARACTER_COLORS
        var BACKPACK = new Color(0.44f, 0.47f, 0.34f);
        var BACKPACK_TIP = new Color(0.39f, 0.31f, 0.25f);
        var BACKPACK_HANDLE = BACKPACK_TIP;
        var HAT_DOWN = new Color(0.92f, 0.85f, 0.71f);
        var HAT_UP = new Color(0.97f, 0.9f, 0.76f);
        var SHOULDER = HAT_UP;
        var SHOULDER_TIP = new Color(0.95f, 0.88f, 0.75f);
        #endregion

        #region BACKPACK
        GL.Color(BACKPACK);
        GL.Vertex3((-3 * proportion) + personagemJogoX, (1.2f * proportion * direcao) + personagemJogoY, 0);
        GL.Vertex3((11 * proportion) + personagemJogoX, (1.2f * proportion * direcao) + personagemJogoY, 0);
        GL.Vertex3((11 * proportion) + personagemJogoX, (-1.7f * proportion * direcao) + personagemJogoY, 0);
        GL.Vertex3((-3 * proportion) + personagemJogoX, (-1.7f * proportion * direcao) + personagemJogoY, 0);

        GL.Color(BACKPACK_TIP);
        GL.Vertex3(11 * proportion + personagemJogoX, 1.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(12 * proportion + personagemJogoX, 1.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(12 * proportion + personagemJogoX, -1.7f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(11 * proportion + personagemJogoX, -1.7f * proportion * direcao + personagemJogoY, 0);

        GL.Vertex3(-3 * proportion + personagemJogoX, 1.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(-4 * proportion + personagemJogoX, 1.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(-4 * proportion + personagemJogoX, -1.7f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(-3 * proportion + personagemJogoX, -1.7f * proportion * direcao + personagemJogoY, 0);
        #endregion

        #region HAT
        GL.Color(HAT_DOWN);
        GL.Vertex3(0 * proportion + personagemJogoX, 0.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(0 * proportion + personagemJogoX, 7.8f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(8 * proportion + personagemJogoX, 7.8f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(8 * proportion + personagemJogoX, 0.2f * proportion * direcao + personagemJogoY, 0);

        GL.Color(HAT_UP);
        GL.Vertex3(1.8f * proportion + personagemJogoX, 1.8f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(1.8f * proportion + personagemJogoX, 6.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(6.2f * proportion + personagemJogoX, 6.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(6.2f * proportion + personagemJogoX, 1.8f * proportion * direcao + personagemJogoY, 0);
        #endregion

        #region LEFT_SHOULDER
        GL.Color(SHOULDER);
        GL.Vertex3(0f * proportion + personagemJogoX, 1.8f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(-1.5f * proportion + personagemJogoX, 1.8f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(-1.5f * proportion + personagemJogoX, 4.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(0f * proportion + personagemJogoX, 4.2f * proportion * direcao + personagemJogoY, 0);

        GL.Color(SHOULDER_TIP);
        GL.Vertex3(-1.5f * proportion + personagemJogoX, 1.8f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(-2f * proportion + personagemJogoX, 1.8f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(-2f * proportion + personagemJogoX, 4.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(-1.5f * proportion + personagemJogoX, 4.2f * proportion * direcao + personagemJogoY, 0);
        #endregion

        #region RIGHT_SHOULDER
        GL.Color(SHOULDER);
        GL.Vertex3(8 * proportion + personagemJogoX, 1.8f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(9.5f * proportion + personagemJogoX, 1.8f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(9.5f * proportion + personagemJogoX, 4.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(8 * proportion + personagemJogoX, 4.2f * proportion * direcao + personagemJogoY, 0);

        GL.Color(SHOULDER_TIP);
        GL.Vertex3(9.5f * proportion + personagemJogoX, 1.8f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(10 * proportion + personagemJogoX, 1.8f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(10 * proportion + personagemJogoX, 4.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(9.5f * proportion + personagemJogoX, 4.2f * proportion * direcao + personagemJogoY, 0);
        #endregion

        #region BACKPACK_HANDLE
        GL.Color(BACKPACK_HANDLE);
        GL.Vertex3(0f * proportion + personagemJogoX, 1.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(-0.5f * proportion + personagemJogoX, 1.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(-0.5f * proportion + personagemJogoX, 4.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(0f * proportion + personagemJogoX, 4.2f * proportion * direcao + personagemJogoY, 0);

        GL.Vertex3(8 * proportion + personagemJogoX, 1.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(8.5f * proportion + personagemJogoX, 1.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(8.5f * proportion + personagemJogoX, 4.2f * proportion * direcao + personagemJogoY, 0);
        GL.Vertex3(8 * proportion + personagemJogoX, 4.2f * proportion * direcao + personagemJogoY, 0);
        #endregion

        EndGLPopMatrix();
    }

    private void PersonagemJogo_Horizontal()
    {
        StartGL_Quads();

        #region CHARACTER_COLORS
        var BACKPACK = new Color(0.44f, 0.47f, 0.34f);
        var BACKPACK_TIP = new Color(0.39f, 0.31f, 0.25f);
        var BACKPACK_HANDLE = BACKPACK_TIP;
        var HAT_DOWN = new Color(0.92f, 0.85f, 0.71f);
        var HAT_UP = new Color(0.97f, 0.9f, 0.76f);
        var SHOULDER = HAT_UP;
        var SHOULDER_TIP = new Color(0.95f, 0.88f, 0.75f);
        #endregion

        #region BACKPACK
        GL.Color(BACKPACK);
        GL.Vertex3((-3 * proportion * direcao) + personagemJogoX, (-3f * proportion) + personagemJogoY, 0);
        GL.Vertex3((-3 * proportion * direcao) + personagemJogoX, (11 * proportion) + personagemJogoY, 0);
        GL.Vertex3((1.2f * proportion * direcao) + personagemJogoX, (11 * proportion) + personagemJogoY, 0);
        GL.Vertex3((1.2f * proportion * direcao) + personagemJogoX, (-3f * proportion) + personagemJogoY, 0);

        GL.Color(BACKPACK_TIP);
        GL.Vertex3(-3 * proportion * direcao + personagemJogoX, 11 * proportion + personagemJogoY, 0);
        GL.Vertex3(-3 * proportion * direcao + personagemJogoX, 12 * proportion + personagemJogoY, 0);
        GL.Vertex3(1.2f * proportion * direcao + personagemJogoX, 12 * proportion + personagemJogoY, 0);
        GL.Vertex3(1.2f * proportion * direcao + personagemJogoX, 11 * proportion + personagemJogoY, 0);

        GL.Vertex3(-3 * proportion * direcao + personagemJogoX, -3f * proportion + personagemJogoY, 0);
        GL.Vertex3(-3 * proportion * direcao + personagemJogoX, -4f * proportion + personagemJogoY, 0);
        GL.Vertex3(1.2f * proportion * direcao + personagemJogoX, -4f * proportion + personagemJogoY, 0);
        GL.Vertex3(1.2f * proportion * direcao + personagemJogoX, -3f * proportion + personagemJogoY, 0);

        #endregion

        #region HAT
        GL.Color(HAT_DOWN);
        GL.Vertex3(0 * proportion * direcao + personagemJogoX, 0.2f * proportion + personagemJogoY, 0);
        GL.Vertex3(7.8f * proportion * direcao + personagemJogoX, 0.2f * proportion + personagemJogoY, 0);
        GL.Vertex3(7.8f * proportion * direcao + personagemJogoX, 8 * proportion + personagemJogoY, 0);
        GL.Vertex3(0 * proportion * direcao + personagemJogoX, 8 * proportion + personagemJogoY, 0);

        GL.Color(HAT_UP);
        GL.Vertex3(1.8f * proportion * direcao + personagemJogoX, 1.8f * proportion + personagemJogoY, 0);
        GL.Vertex3(1.8f * proportion * direcao + personagemJogoX, 6.2f * proportion + personagemJogoY, 0);
        GL.Vertex3(6.2f * proportion * direcao + personagemJogoX, 6.2f * proportion + personagemJogoY, 0);
        GL.Vertex3(6.2f * proportion * direcao + personagemJogoX, 1.8f * proportion + personagemJogoY, 0);
        #endregion

        #region LEFT_SHOULDER
        GL.Color(SHOULDER);
        GL.Vertex3(1.8f * proportion * direcao + personagemJogoX, 8 * proportion + personagemJogoY, 0);
        GL.Vertex3(1.8f * proportion * direcao + personagemJogoX, 9.5f * proportion + personagemJogoY, 0);
        GL.Vertex3(4.2f * proportion * direcao + personagemJogoX, 9.5f * proportion + personagemJogoY, 0);
        GL.Vertex3(4.2f * proportion * direcao + personagemJogoX, 8 * proportion + personagemJogoY, 0);

        GL.Color(SHOULDER_TIP);
        GL.Vertex3(1.8f * proportion * direcao + personagemJogoX, 9.5f * proportion + personagemJogoY, 0);
        GL.Vertex3(1.8f * proportion * direcao + personagemJogoX, 10 * proportion + personagemJogoY, 0);
        GL.Vertex3(4.2f * proportion * direcao + personagemJogoX, 10 * proportion + personagemJogoY, 0);
        GL.Vertex3(4.2f * proportion * direcao + personagemJogoX, 9.5f * proportion + personagemJogoY, 0);
        #endregion

        #region RIGHT_SHOULDER
        GL.Color(SHOULDER);
        GL.Vertex3(1.8f * proportion * direcao + personagemJogoX, 0.2f * proportion + personagemJogoY, 0);
        GL.Vertex3(1.8f * proportion * direcao + personagemJogoX, -1.3f * proportion + personagemJogoY, 0);
        GL.Vertex3(4.2f * proportion * direcao + personagemJogoX, -1.3f * proportion + personagemJogoY, 0);
        GL.Vertex3(4.2f * proportion * direcao + personagemJogoX, 0.2f * proportion + personagemJogoY, 0);

        GL.Color(SHOULDER_TIP);
        GL.Vertex3(1.8f * proportion * direcao + personagemJogoX, -1.3f * proportion + personagemJogoY, 0);
        GL.Vertex3(1.8f * proportion * direcao + personagemJogoX, -1.8f * proportion + personagemJogoY, 0);
        GL.Vertex3(4.2f * proportion * direcao + personagemJogoX, -1.8f * proportion + personagemJogoY, 0);
        GL.Vertex3(4.2f * proportion * direcao + personagemJogoX, -1.3f * proportion + personagemJogoY, 0);
        #endregion

        #region BACKPACK_HANDLE
        GL.Color(BACKPACK_HANDLE);
        GL.Vertex3(1.2f * proportion * direcao + personagemJogoX, 8 * proportion + personagemJogoY, 0);
        GL.Vertex3(4.2f * proportion * direcao + personagemJogoX, 8 * proportion + personagemJogoY, 0);
        GL.Vertex3(4.2f * proportion * direcao + personagemJogoX, 8.5f * proportion + personagemJogoY, 0);
        GL.Vertex3(1.2f * proportion * direcao + personagemJogoX, 8.5f * proportion + personagemJogoY, 0);

        GL.Vertex3(1.2f * proportion * direcao + personagemJogoX, 0.2f * proportion + personagemJogoY, 0);
        GL.Vertex3(4.2f * proportion * direcao + personagemJogoX, 0.2f * proportion + personagemJogoY, 0);
        GL.Vertex3(4.2f * proportion * direcao + personagemJogoX, -0.3f * proportion + personagemJogoY, 0);
        GL.Vertex3(1.2f * proportion * direcao + personagemJogoX, -0.3f * proportion + personagemJogoY, 0);
        #endregion

        EndGLPopMatrix();
    }

    private void StartGL_Quads()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
    }

    private static void EndGLPopMatrix()
    {
        GL.End();
        GL.PopMatrix();
    }
}