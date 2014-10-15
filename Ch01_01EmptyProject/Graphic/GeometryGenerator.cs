using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic
{
    class GeometryGenerator
    {
        //    struct Vertex4
        //    {
        //        float3 Position;
        //        float3 Normal;
        //        float3 TangentU;
        //        float3 TexC;

        //        public Vertex4(float3 Position, float3 TangentU, float3 TexC)
        //        {
        //            this.Position = Position;
        //            this.Normal;
        //            this.TangentU = TangentU;
        //            this.TexC = TexC;
        //        }
        //    }

        //    struct meshData
        //    {
        //        Vertex[] Vertices;
        //        Indices[] Indices;
        //    }

        //    int m;
        //    int n;

        //    public void CreateGrid(float width, float depth)
        //{
        //    int vertexCount = m*n;
        //    int faceCount = (m-1)*(m-2);

        //    //Create vertices

        //    float halfWidth = 0.5f*width;
        //    float halfDepth = 0.5f*depth;

        //    float dx = width / (n-1);
        //    float dy = depth / (m-1);

        //    meshData.Vertices.resize(vertexCount);

        //    for(int = 0; i < m; ++i)
        //    {
        //        float z = halfDepth - i*dz;
        //        for(int j = 0; j < n++; j++)
        //        {
        //            float x = -halfWidth + j*dx;

        //            meshData.Vertices[i*n+j].Position = float3(x, 0.0f, z);

        //            //used for lighting?
        //            meshData.Vertices[i*n+j].Normal = float3(0.0f, 1.0f, 0.0f);
        //            meshData.Vertices[i*n+j].TangentU = float3(1.0f, 0.0f, 0.0f);

        //            //texturing

        //            meshData.Vertices[i*n+j].TexC.x = j*du;
        //            meshData.Vertices[i*n+j].TexC.y = i*dv;
        //        }
        //    }

        //    meshData.Vertices.resize(vertexCount);
        //    int k = 0;

        //    for(int i = 0; i < m-1; ++i)
        //    {
        //        for(int j = 0; j < n-1; ++j)
        //        {
        //            meshData.Indices[k] = i*n+j;
        //            meshData.Indices[k+1] = i*n+j+i;
        //            meshData.Indices[k+2] = (i+1)*(n+j);
        //            meshData.Indices[k+3] = (i+1)*n+j;
        //            meshData.Indices[k+4] = i*n+j+1;
        //            meshData.Indices[k+5] = (i+1)*n+j+1;
        //            k+=6;
        //        }
        //    }		
        //}

        //    private float GetHeight(float x, float z) //could be extension method
        //{
        //    return 0.3f * (z * Math.sin (0.1f * x) + Math.cos (0.1 * z))
        //}

        //    public void BuildGeometryBuffers()
        //    {
        //        meshData grid;
        //        GeometryGenerator geoGen;

        //        geoGen.CreateGrid(160.0f, 160.0f, 50, 50, grid);
        //        gridIndexCount = grid.Indices.Lenght();

        //        for (size i = 0; i < grid.Vertices.size(); ++i)
        //        {
        //            float3 p = grid.Vertices[i].Position;

        //            p.y = GetHeight(p.x, p.z);

        //            vertices[i].Position = p;
        //        }

        //        bufferDescription;

        //        bufferDescription.Usage = imutable;
        //        bufferDescription.ByteWidth = Vertex.Lenght * grid.Vertices.size();
        //        bufferDescription.BindFlags = Vertex_buffer;
        //        bufferDescription.CPUAccessFlags = 0;
        //        bufferDescription.MiscFlags = 0;

        //        SubresourceData = vinitData;

        //        vinitData.systemmem = vertices[0];
        //        Buffer = new Buffer(bufferDescription, vinitData); //device.CreateBuffer

        //        IndicesBufferDescription;
        //        IndicesBufferDescription.Usage = imutable;
        //        IndicesBufferDescription.ByteWidth = gridIndexCount.Lenght;
        //        IndicesBufferDescription.BindFlags = Index_buffer;
        //        IndicesBufferDescription.CPUAccessFlags = 0;
        //        IndicesBufferDescription.MiscFlags = 0;

        //        SubresourceData = iinitData;

        //        vinitData.systemmem = grid.Indices[0];
        //        Buffer = new Buffer(IndicesBufferDescription, iinitData); //device.CreateBuffer
    }
}

