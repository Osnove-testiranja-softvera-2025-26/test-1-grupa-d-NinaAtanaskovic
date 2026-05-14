
namespace OTS2026_GrupaD.Models
{
    public enum TileType
    {
        Standard,
        MapBarrier,
        Hive
    }

    public enum TileContent
    {
        Empty,
        Flower,
        Bee
    }

    public class Tile
    {
        public TileContent Content { get; set; }
        public TileType Type { get; set; }

        public Tile()
        {
            Content = TileContent.Empty;
            Type = TileType.Standard;
        }

        public Tile(TileContent content)
        {
            Content = content;
        }
    }
}
