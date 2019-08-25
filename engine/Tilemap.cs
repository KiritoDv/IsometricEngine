using System.Diagnostics;

namespace IsometricEngine.engine{
    public class Tilemap{
        public ITile[,,] points;
        public int mapX;
        public int mapY;
        public int mapZ;

        public Tilemap(int mapX, int mapY, int mapZ){
            this.mapX = mapX;
            this.mapY = mapY;
            this.mapZ = mapZ;
            this.points = new ITile[mapX, mapY, mapZ];
        }

        public void setTile(int x, int y, int z, ITile tile){
            this.points[x, y, z] = tile;
        }
    }
}