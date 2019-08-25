namespace IsometricEngine.engine
{
    public class ITile{
        public string tileTexture;
        public int width;
        public int height;
        public ITile(string tileTexture, int width, int height){
            this.tileTexture = tileTexture;
            this.height = width;
            this.width = height;
        }
        
    }
}