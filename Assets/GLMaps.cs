using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLMaps : MonoBehaviour
{
   public Material mat;
   public Vector2 sb;
   float by = 0;
   float bx = 0;
   float velo = 0.01f;
   float direcao = 1;
   float movX = 0;
   float movY = 0;
   public bool horizontal = true; 


   public void Start() {
       sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
   }


   public void Update() {
       sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = true;
            movX -= velo;
            direcao = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = true;
            movX += velo;
            direcao = 1;
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            horizontal = false;
            movY += velo;
            direcao = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            horizontal = false;
            movY -= velo;
            direcao = -1;
        }
   }

    private void OnPostRender() {

        BarDown();
        BarRight();

        if (horizontal)
        {
            Boneco();
        } else
        {
            Boneco2();
        }
        
    }


    void BarDown() {

        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.green);
        //mapa
        GL.Vertex3(sb.x * (-1), sb.y * (-1),0);
        GL.Vertex3(sb.x * (-1), sb.y * (-1) + 1, 0);
        GL.Vertex3(sb.x ,sb.y * (-1) + 1, 0);
        GL.Vertex3(sb.x, sb.y * (-1),0);
        GL.End();
        GL.PopMatrix();
    }
    void BarRight()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.green);
        GL.Vertex3(sb.x, sb.y * (-1), 0);
        GL.Vertex3(sb.x, sb.y, 0);
        GL.Vertex3(sb.x - 1, sb.y, 0);
        GL.Vertex3(sb.x - 1, sb.y * (-1), 0);
        GL.End();
        GL.PopMatrix();
    }

    void Boneco(){
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.red);

        GL.Vertex3(bx * direcao + movX, by  + movY, 0);
        GL.Vertex3(bx+1 * direcao + movX, by+0.5f  + movY, 0);
        GL.Vertex3(bx+1 * direcao + movX, by+0.5f  + movY, 0);
        GL.Vertex3(bx * direcao + movX, by+1  + movY, 0);
        GL.Vertex3(bx * direcao + movX, by+1  + movY, 0);
        GL.Vertex3(bx * direcao + movX, by  + movY, 0);

        GL.End();
        GL.PopMatrix();
    }

    void Boneco2() {

        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.red);
        GL.Vertex3(bx  + movX, by* direcao  + movY , 0);
        GL.Vertex3(bx+1  + movX, by* direcao  + movY, 0);
        GL.Vertex3(bx+1  + movX, by* direcao  + movY, 0);
        GL.Vertex3(bx+0.5f  + movX, by+1* direcao  + movY, 0);
        GL.Vertex3(bx+0.5f  + movX, by+1* direcao  + movY, 0);
        GL.Vertex3(bx  + movX, by* direcao  + movY, 0);
        GL.End();
        GL.PopMatrix();
    }
}