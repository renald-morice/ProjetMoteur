using System;
using System.Drawing;
using System.Globalization;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Utils
{
    // FIXME: Still not happy how this turns out.
    //  handle just be an index into a table or something.
    public static class VBOUtils
    {
        public struct Vbo<TVertex> where TVertex : struct
        {
            public int VboID, EboID, NumElements;
            public TVertex[] vertices;
        }
        
        public static Vbo<TVertex> CreateVBO<TVertex>(TVertex[] vertices, short[] elements) where TVertex : struct
        {
            var handle = new Vbo<TVertex>();
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
            handle.vertices = vertices;
            
            return handle;
        }

        public static void DrawVBO<TVertex>(PrimitiveType mode, ref Vbo<TVertex> handle) where TVertex : struct
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

            GL.VertexPointer(3, VertexPointerType.Float, BlittableValueType.StrideOf(handle.vertices), new IntPtr(0));
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, BlittableValueType.StrideOf(handle.vertices), new IntPtr(12));
           
            GL.DrawElements(mode, handle.NumElements, DrawElementsType.UnsignedShort, IntPtr.Zero);
        }

        public static Vbo<VertexPositionColor> CubeVbo = CreateVBO(
            new VertexPositionColor[]
            {   new VertexPositionColor(-1.0f, -1.0f,  1.0f, Color.DarkViolet),
                new VertexPositionColor( 1.0f, -1.0f,  1.0f, Color.DarkRed),
                new VertexPositionColor( 1.0f,  1.0f,  1.0f, Color.Yellow),
                new VertexPositionColor(-1.0f,  1.0f,  1.0f, Color.Gold),
                new VertexPositionColor(-1.0f, -1.0f, -1.0f, Color.DarkGreen),
                new VertexPositionColor( 1.0f, -1.0f, -1.0f, Color.DarkBlue),
                new VertexPositionColor( 1.0f,  1.0f, -1.0f, Color.Orange),
                new VertexPositionColor(-1.0f,  1.0f, -1.0f, Color.Bisque)
            },
            new short[]
            {
                0, 1, 2, 2, 3, 0, // front face
                3, 2, 6, 6, 7, 3, // top face
                7, 6, 5, 5, 4, 7, // back face
                4, 0, 3, 3, 7, 4, // left face
                0, 1, 5, 5, 4, 0, // bottom face
                1, 5, 6, 6, 2, 1, // right face
            });
    }
}