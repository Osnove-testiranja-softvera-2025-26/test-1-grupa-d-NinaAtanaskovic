using NUnit.Framework;
using OTS2026_GrupaD.Exceptions;
using OTS2026_GrupaD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace OTS2026_GrupaD.Test
{
    [TestFixture]
    internal class GameTest
    {
        private Game game;

        [SetUp]
        public void SetUp()
        {
            game = new Game(new Location(0, 0, 0), new Location(30, 30, 30));

        }


        public Game(Location playerLocation, Location beeLocation)
        {
            Map = new Space();
            Map.InitializeMap();

            if (!ValidateLocationInsideMap(playerLocation) || !ValidateLocationInsideMap(beeLocation))
            {
                throw new LocationOutsideOfMapException("Locations must be valid!");
            }

            int itemX = beeLocation.X;
            int itemY = beeLocation.Y;
            int itemZ = beeLocation.Z;

            Map.Tiles[itemX, itemY, itemZ].Content = TileContent.Bee;
            Player = new Player(playerLocation);
        }

        //inicijalizacija
        //validne pozicije za x su od [0-30] bez [5-9]jer imamo pravougaonik 5*20*10 i bez [20-24]
        //validne pozicije za y [0-30] bez [10-29](racunamo i 10 pa je za 20 vise 29) i bez [0-20]
        //validne za z [0-30] bez [0-9]

        // nevalidne klase; preko 30 za jednu od koordinata , kombinacija koordinata koje upadaju u opseg gore naveden
        [TestCase(31,31,31)]
        [TestCase(6,15,8)]//nevalidna klasa ekvivalencije upada u jedan od  pravougaonik
        [TestCase(-1,-1,1)]
        public void InicijalizacijaPozicijaIgracaIzvanMape_Exeption(int x, int y, int z)
        {
            Location player= new Location(x, y, z);
            Exception ex = Assert.Throws<LocationOutsideOfMapException>((TestDelegate)(() => new Game(player, new Location())));
            Assert.That(ex.Message, Is.EqualTo("Locations must be valid!"));

        }
        [TestCase(31, 31, 31)]
        [TestCase(6, 15, 8)]//nevalidna klasa ekvivalencije upada u jedan od  pravougaonik
        [TestCase(-1, -1, 1)]
        public void InicijalizacijaPozicijaBeeIzvanMape_Exeption(int x, int y, int z)
        {
            Location bee = new Location(x, y, z);
            Exception ex = Assert.Throws<LocationOutsideOfMapException>((TestDelegate)(() => new Game(new Location(), bee)));
            Assert.That(ex.Message, Is.EqualTo("Locations must be valid!"));

        }

        //f2 move player 

      

        //KLASE EKVIVALENCIJE: ZA POZICIJU PLAYERA VAZI DA MORA DA BUDE NA POZICIJAMA SVIH KOORDINATA [0-29] S TIM A NE SME BITI NULA KOORDINATA NA KOORDINATI KOJA SE UMANJUJE
        //NEVALIDNE KOORDINATE SU 0(TAMO GDE SE PRILIKOM MOVE SMANJUJE) I KOORDINATE SU IZVAN MAPE 
        [TestCase(2,2,2)]
        public void Moveplayer_Gore(int x, int y, int z)
        {
           
           Game game= new Game(new Location(x, y, z), new Location());
            Location exp= new Location(x, y-1, z);
            game.MovePlayer(Direction.Up);
            Assert.That(game.Player.Location, Is.EqualTo(exp));
        }

        [TestCase(2, 2, 2)]
        public void Moveplayer_dOLE(int x, int y, int z)
        {

            Game game = new Game(new Location(x, y, z), new Location());
            Location exp = new Location(x, y + 1, z);
            game.MovePlayer(Direction.Down);
            Assert.That(game.Player.Location, Is.EqualTo(exp));
        }
        [TestCase(2, 2, 2)]
        public void Moveplayer_LEVO(int x, int y, int z)
        {

            Game game = new Game(new Location(x, y, z), new Location());
            Location exp = new Location(x-1, y , z);
            game.MovePlayer(Direction.Left);
            Assert.That(game.Player.Location, Is.EqualTo(exp));
        }
        //F4 
        public void UpdatePlayer()
        {
            int x = Player.Location.X;
            int y = Player.Location.Y;
            int z = Player.Location.Z;

            if (Map.Tiles[x, y, z].Content.Equals(TileContent.Flower))
            {
                Player.AmountOfFlowers++;
            }
            else if (Map.Tiles[x, y, z].Content.Equals(TileContent.Bee))
            {
                Player.BeeCollected = true;
            }
            else if (Map.Tiles[x, y, z].Type.Equals(TileType.Hive))
            {
                Player.AmountOfHoneyJars += Player.AmountOfFlowers;
                Player.AmountOfFlowers = 0;
            }

            Map.EmptyTileOnLocation(Player.Location);
        }

        [TestCase(3,3,3,1)]
        public void UpdatePosition_flowerPolje(int x, int y, int z , int expCountofFlowers)
        {
            Game game = new Game(new Location(x, y, z), new Location());
            game.Map.Tiles[x,y,z].Content= TileContent.Flower;
            game.UpdatePlayer();
            Assert.That(game.Player.AmountOfFlowers, Is.EqualTo(expCountofFlowers));
        }
        [TestCase(3, 3, 3, 1, 2)]
        public void UpdatePlayer_TileTypeHileKolicinaMeda(int x, int y, int z, int cvece, int expMed)
        {
            Game game = new Game(new Location(x, y, z), new Location());
            game.Player.AmountOfFlowers = cvece;
            game.Map.Tiles[x, y, z].Type = TileType.Hive;
            game.UpdatePlayer();
            Assert.That(game.Player.AmountOfHoneyJars, Is.EqualTo(expMed));

        }

        
    }
}
