using EcoMatrix.Core.Abstracts;
using EcoMatrix.Core.Utils;
using OpenTK.Graphics.OpenGL4;

namespace EcoMatrix.Core.ArrayObjects
{
    public class DefaultVertexArray : VertexArrayObject
    {
        public override void ApplyAttributes()
        {
            // position attribute
            GL.VertexAttribPointer(index: 0,
                                   size: Global.PositionAttributeSize,
                                   type: VertexAttribPointerType.Float,
                                   normalized: false,
                                   stride: Global.AllShaderAttributeSize * sizeof(float),
                                   offset: 0);
            GL.EnableVertexAttribArray(0);

            // color attribute
            GL.VertexAttribPointer(index: 1,
                                   size: Global.ColorAttributeSize,
                                   type: VertexAttribPointerType.Float,
                                   normalized: false,
                                   stride: Global.AllShaderAttributeSize * sizeof(float),
                                   offset: Global.PositionAttributeSize * sizeof(float));
            GL.EnableVertexAttribArray(1);

            // normal attribute
            GL.VertexAttribPointer(index: 2,
                                   size: Global.NormalAttributeSize,
                                   type: VertexAttribPointerType.Float,
                                   normalized: false,
                                   stride: Global.AllShaderAttributeSize * sizeof(float),
                                   offset: (Global.PositionAttributeSize + Global.ColorAttributeSize) * sizeof(float));
            GL.EnableVertexAttribArray(2);


            // texture coordinates attribute
            GL.VertexAttribPointer(index: 3,
                                   size: Global.TextureCoordinateAttributeSize,
                                   type: VertexAttribPointerType.Float,
                                   normalized: false,
                                   stride: Global.AllShaderAttributeSize * sizeof(float),
                                   offset: (Global.PositionAttributeSize + Global.ColorAttributeSize + Global.NormalAttributeSize) * sizeof(float));
            GL.EnableVertexAttribArray(3);
        }

    }
}