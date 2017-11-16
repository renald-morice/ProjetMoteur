using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine
{
    // NOTE(francois): Well... This does not draw a cube... But it's _way_ harder!
    //  (and it uses VBO, which should be implemented later)
    public class CubeRenderer : GameComponent, IRenderComponent
    {

        struct Vbo { public int VboID, EboID, NumElements; }

        VertexPositionColor[] CubeVertices = new VertexPositionColor[]
        {   new VertexPositionColor(-1.0f, -1.0f,  1.0f, Color.DarkRed),
            new VertexPositionColor( 1.0f, -1.0f,  1.0f, Color.DarkRed),
            new VertexPositionColor( 1.0f,  1.0f,  1.0f, Color.Gold),
            new VertexPositionColor(-1.0f,  1.0f,  1.0f, Color.Gold),
            new VertexPositionColor(-1.0f, -1.0f, -1.0f, Color.DarkRed),
            new VertexPositionColor( 1.0f, -1.0f, -1.0f, Color.DarkRed),
            new VertexPositionColor( 1.0f,  1.0f, -1.0f, Color.Gold),
            new VertexPositionColor(-1.0f,  1.0f, -1.0f, Color.Gold)
        };
        readonly short[] CubeElements = new short[]
        {
            0, 1, 2, 2, 3, 0, // front face
            3, 2, 6, 6, 7, 3, // top face
            7, 6, 5, 5, 4, 7, // back face
            4, 0, 3, 3, 7, 4, // left face
            0, 1, 5, 5, 4, 0, // bottom face
            1, 5, 6, 6, 2, 1, // right face
        };
        


        // FIXME: If we do everything this way, the engine will be _really_ slow.
        //  But for now, it is better than nothing!
        public void Render()
        {
            GL.ClearColor(System.Drawing.Color.MidnightBlue);
            GL.Enable(EnableCap.DepthTest);
            Vbo vbo = LoadVBO(CubeVertices, CubeElements);
            Draw(vbo);

            /*
              GL.Begin(PrimitiveType.Triangles);

              GL.Color3(Color.MidnightBlue);
              GL.Vertex2(-1.0f, 1.0f);
              GL.Color3(Color.SpringGreen);
              GL.Vertex2(-1.0f, -1.0f);
              GL.Color3(Color.IndianRed);
              GL.Vertex2(1.0f, 1.0f);

              GL.Color3(Color.IndianRed);
              GL.Vertex2(1.0f, 1.0f);
              GL.Color3(Color.Ivory);
              GL.Vertex2(1.0f, -1.0f);
              GL.Color3(Color.SpringGreen);
              GL.Vertex2(-1.0f, -1.0f);

              GL.End();
              */
        }

        Vbo LoadVBO<TVertex>(TVertex[] vertices, short[] elements) where TVertex : struct
        {
            Vbo handle = new Vbo();
            int size;

            // To create a VBO:
            // 1) Generate the buffer handles for the vertex and element buffers.
            // 2) Bind the vertex buffer handle and upload your vertex data. Check that the buffer was uploaded correctly.
            // 3) Bind the element buffer handle and upload your element data. Check that the buffer was uploaded correctly.

            GL.GenBuffers(1, out handle.VboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, handle.VboID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * BlittableValueType.StrideOf(vertices)), vertices,
                          BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);
            if (vertices.Length * BlittableValueType.StrideOf(vertices) != size)
                throw new ApplicationException("Vertex data not uploaded correctly");

            GL.GenBuffers(1, out handle.EboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle.EboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(elements.Length * sizeof(short)), elements,
                          BufferUsageHint.StaticDraw);
            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
            if (elements.Length * sizeof(short) != size)
                throw new ApplicationException("Element data not uploaded correctly");

            handle.NumElements = elements.Length;
            return handle;
        }

        void Draw(Vbo handle)
        {
            // To draw a VBO:
            // 1) Ensure that the VertexArray client state is enabled.
            // 2) Bind the vertex and element buffer handles.
            // 3) Set up the data pointers (vertex, normal, color) according to your vertex format.
            // 4) Call DrawElements. (Note: the last parameter is an offset into the element buffer
            //    and will usually be IntPtr.Zero).

            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.VertexArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, handle.VboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle.EboID);

            GL.VertexPointer(3, VertexPointerType.Float, BlittableValueType.StrideOf(CubeVertices), new IntPtr(0));
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, BlittableValueType.StrideOf(CubeVertices), new IntPtr(12));

#pragma warning disable CS0618 // Le type ou le membre est obsolète
            GL.DrawElements(BeginMode.Triangles, handle.NumElements, DrawElementsType.UnsignedShort, IntPtr.Zero);
#pragma warning restore CS0618 // Le type ou le membre est obsolète
        }

     
    }
}