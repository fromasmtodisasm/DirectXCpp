using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    public class Textures : ICollection<ShaderResourceView>, IDisposable
    {
        //ugly
        private TextureDefinitions td = new TextureDefinitions();
        private ShaderResourceView textureResource;
        private Device device;

        public List<ShaderResourceView> TextureList { get; set; }
        //enum with textures + dictionary with fileNames ?
        public Textures(Device device, TextureType[] fileNames)
        {
            TextureList = new List<ShaderResourceView>();
            this.device = device;

            foreach (TextureType textureType in fileNames)
            {
                ShaderResourceView srw = TextureLoader.Load(device, td[textureType]);
                this.Add(srw);
            }
        } 
        //public void Add(Textures texture)
        //{
        //    string t = td[texture];
        //    this.Add(t);
        //}

        public void Add(ShaderResourceView item)
        {
            TextureList.Add(item);
        }

        public void Clear()
        {
            foreach (var texture in TextureList)
            {
                texture.Dispose();
            }
        }

        public bool Contains(ShaderResourceView item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ShaderResourceView[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return TextureList.Count; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(ShaderResourceView item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ShaderResourceView> GetEnumerator()
        {
            return TextureList.GetEnumerator();
        }

        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Dispose()
        {
            this.Clear();
        }
    }
}
