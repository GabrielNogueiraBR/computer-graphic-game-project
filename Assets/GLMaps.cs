using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GLMaps : MonoBehaviour
{
    public Material mat;
    public Vector2 sb;
    float velo = 0.03f;
    public float direcao = 1;
    public Sentido sentidoMovimentacao = Sentido.Horizontal;
    private float proportion = 0.1f;
    public float personagemJogoX = 0;
    public float personagemJogoY = 0;
    public List<Parede> ParedesMapa = new List<Parede>();

    float larguraParede = 0.5f;
    float larguraCaminho = 2.5f;

    public float coordenadaInicialX = 0;
    public float coordenadaInicialY = 0;
    public float coordenadaInicialZ = 0;

    public void Start() {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
               
    }


    public void Update() {
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            sentidoMovimentacao = Sentido.Horizontal;
            direcao = -1;
            if (!ColisaoHorizontal())
            {
                personagemJogoX -= velo;
                transform.position -= new Vector3(velo,0,0);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            sentidoMovimentacao = Sentido.Horizontal;
            direcao = 1;

            if (!ColisaoHorizontal())
            {
                personagemJogoX += velo;
                transform.position += new Vector3(velo, 0, 0);
            }
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {
            sentidoMovimentacao = Sentido.Vertical;
            direcao = 1;

            if (!ColisaoVertical())
            {
                personagemJogoY += velo;
                transform.position += new Vector3(0, velo, 0);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            sentidoMovimentacao = Sentido.Vertical;
            direcao = -1;

            if (!ColisaoVertical())
            {
                personagemJogoY -= velo;
                transform.position -= new Vector3(0, velo, 0);
            }
        }
    }

    private bool ColisaoHorizontal()
    {
        return ParedesMapa.Where(parede =>
        {
            Ponto v1 = parede.Vertice1;
            Ponto v2;

            if ((parede.Vertice1.Y != parede.Vertice2.Y) && (parede.Vertice1.X != parede.Vertice2.X))
            {
                v2 = parede.Vertice2;
            }
            else if ((parede.Vertice1.Y != parede.Vertice3.Y) && (parede.Vertice1.X != parede.Vertice3.X))
            {
                v2 = parede.Vertice3;
            }
            else
            {
                v2 = parede.Vertice4;
            }

            bool vertical = ((personagemJogoY >= v1.Y) && (personagemJogoY <= v2.Y)) || ((personagemJogoY >= v2.Y) && (personagemJogoY <= v1.Y));

            bool horizontal = ((personagemJogoX + (velo * direcao) >= v1.X) && (personagemJogoX + (velo * direcao) <= v2.X)) || ((personagemJogoX + (velo * direcao) >= v2.X) && (personagemJogoX + (velo * direcao) <= v1.X));

            if (vertical && horizontal)
                return true;
            return false;
        }
        ).Any();
    }

    private bool ColisaoVertical()
    {
        return ParedesMapa.Where(parede =>
        {
            Ponto v1 = parede.Vertice1;
            Ponto v2;

            if ((parede.Vertice1.Y != parede.Vertice2.Y) && (parede.Vertice1.X != parede.Vertice2.X))
            {
                v2 = parede.Vertice2;
            }
            else if ((parede.Vertice1.Y != parede.Vertice3.Y) && (parede.Vertice1.X != parede.Vertice3.X))
            {
                v2 = parede.Vertice3;
            }
            else
            {
                v2 = parede.Vertice4;
            }

            bool vertical = ((personagemJogoY + (velo * direcao) >= v1.Y) && (personagemJogoY + (velo * direcao) <= v2.Y)) || ((personagemJogoY + (velo * direcao) >= v2.Y) && (personagemJogoY + (velo * direcao) <= v1.Y));

            bool horizontal = ((personagemJogoX >= v1.X) && (personagemJogoX  <= v2.X)) || ((personagemJogoX  >= v2.X) && (personagemJogoX  <= v1.X));

            if (vertical && horizontal)
                return true;
            return false;
        }
        ).Any();
    }

    private void OnPostRender() {

        CreateMap();

        if (sentidoMovimentacao == Sentido.Horizontal)
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
        BarDown(coordenadaInicialX,coordenadaInicialY,coordenadaInicialZ);
        BarRight();
        BarLeft();
        BarTop();

    }

    void BarDown(float x, float y, float z)
    {

        #region ParedeUnidade1
        Parede ParedeUnidade1 = new Parede();
        ParedeUnidade1.Sentido = Sentido.Horizontal;
        Ponto ponto = new Ponto(x,y);
        
        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));
        
        ponto.addX(-21);        
        ParedeUnidade1.AdicionarVertice1(ponto);

        ponto.addY(larguraParede);       
        ParedeUnidade1.AdicionarVertice2(ponto);

        ponto.addX(21 - larguraCaminho);        
        ParedeUnidade1.AdicionarVertice3(ponto);

        ponto.addY(-larguraParede);        
        ParedeUnidade1.AdicionarVertice4(ponto);

        ParedeUnidade1.CriarParede();
        
        EndGLPopMatrix();
        ParedesMapa.Add(ParedeUnidade1);
        #endregion
        

        #region ParedeUnidade2
        Parede ParedeUnidade2 = new Parede();
        ParedeUnidade2.Sentido = Sentido.Vertical;
        Ponto ponto2 = new Ponto(x, y);

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));

        ponto2.addX(-larguraCaminho - larguraParede);
        ponto2.addY(larguraParede);
        ParedeUnidade2.AdicionarVertice1(ponto2);

        ponto2.addX(+larguraParede);
        ParedeUnidade2.AdicionarVertice2(ponto2);

        ponto2.addY(7);
        ParedeUnidade2.AdicionarVertice3(ponto2);

        ponto2.addX(-larguraParede);
        ParedeUnidade2.AdicionarVertice4(ponto2);

        ParedeUnidade2.CriarParede();
        
        EndGLPopMatrix();
        ParedesMapa.Add(ParedeUnidade2);

        #endregion


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

        #region ParedeUnidade5
        Parede ParedeUnidade5 = new Parede();
        ParedeUnidade5.Sentido = Sentido.Vertical;
        Ponto ponto5 = new Ponto(x, y);

        StartGL_Quads();
        GL.Color(new Color(0.33f, 0.47f, 0.23f));

        ponto5.addX(14);
        ParedeUnidade5.AdicionarVertice1(ponto5);

        ponto5.addY(larguraParede);
        ParedeUnidade5.AdicionarVertice2(ponto5);

        ponto5.addX(larguraCaminho - 14);
        ParedeUnidade5.AdicionarVertice3(ponto5);

        ponto5.addY(-larguraParede);
        ParedeUnidade5.AdicionarVertice4(ponto5);

        ParedeUnidade5.CriarParede();

        EndGLPopMatrix();
        ParedesMapa.Add(ParedeUnidade5);
        #endregion

        
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

public class Parede
{
    public Ponto Vertice1 { get; set; }
    public Ponto Vertice2 { get; set; }
    public Ponto Vertice3 { get; set; }
    public Ponto Vertice4 { get; set; }
    public Sentido Sentido { get; set; }

    public Parede()
    {

    }

    public Parede(Ponto vertice1, Ponto vertice2, Ponto vertice3, Ponto vertice4, Sentido sentido)
    {
        Vertice1 = vertice1;
        Vertice2 = vertice2;
        Vertice3 = vertice3;
        Vertice4 = vertice4;
        Sentido = sentido;
    }

    public void AdicionarVertice1(float x, float y) => Vertice1 = new Ponto(x, y);
    public void AdicionarVertice1(Ponto ponto) => Vertice1 = new Ponto(ponto);

    public void AdicionarVertice2(float x, float y) => Vertice2 = new Ponto(x, y);
    public void AdicionarVertice2(Ponto ponto) => Vertice2 = new Ponto(ponto);

    public void AdicionarVertice3(float x, float y) => Vertice3 = new Ponto(x, y);
    public void AdicionarVertice3(Ponto ponto) => Vertice3 = new Ponto(ponto);

    public void AdicionarVertice4(float x, float y) => Vertice4 = new Ponto(x, y);
    public void AdicionarVertice4(Ponto ponto) => Vertice4 = new Ponto(ponto);

    public void CriarParede()
    {
        GL.Vertex3(Vertice1.X, Vertice1.Y, 0);
        GL.Vertex3(Vertice2.X, Vertice2.Y, 0);
        GL.Vertex3(Vertice3.X, Vertice3.Y, 0);
        GL.Vertex3(Vertice4.X, Vertice4.Y, 0);
        
    }
}

public class Ponto
{    
    public float X { get; set; }
    public float Y { get; set; }

    public Ponto(float x, float y)
    {
        X = x;
        Y = y;
    }

    public Ponto(Ponto ponto)
    {
        this.X = ponto.X;
        this.Y = ponto.Y;
    }

    public void addX(float value)
    {
        this.X += value;
    }

    public void addY(float value)
    {
        this.Y += value;
    }

}

public enum Sentido
{
    Horizontal,
    Vertical
}