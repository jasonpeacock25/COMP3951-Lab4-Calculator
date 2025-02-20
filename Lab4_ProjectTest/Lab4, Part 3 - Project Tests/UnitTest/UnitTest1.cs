using Project;
using System.Runtime.InteropServices;
namespace UnitTest
{
    /// <summary>
    /// Unit Tests for the GameBoard, Tile, and Buidling classes. 
    /// 
    /// Authors: Caleb, Tanner, Jason, Phyo
    /// Date:    February 19, 2025
    /// Version: 1.0
    /// </summary>
    public class UnitTest1
    {
        /// <summary>
        /// Tests if an empty game board is generated correctly.
        /// </summary>
        [Fact]
        public void TestGenerateGameBoard()
        {
            // Arrange 
            GameBoard gameboard = new GameBoard(1, 1);

            // Act
            gameboard.generateGameBoard();
            List<Tile> ActualTiles = gameboard.getTiles();
            List<Tile> ExpectedTiles = [];

            // Assert
            Assert.Equal(ExpectedTiles, ActualTiles);
        }

        /// <summary>
        /// Tests adding a river building to a tile on the game board.
        /// </summary>
        [Fact]
        public void TestAddRiverBuildingToGameBoardWithBuilding()
        {
            // Arrange
            GameBoard gameboard = new GameBoard(1, 1);
            gameboard.generateGameBoard();
            Tile tile = new Tile(1, 1);
            Building building = new Building(1.0f, new Resource(ResourceType.RIVER));

            // Act
            gameboard.addBuilding(building, tile);

            // Assert
            Assert.Equal(tile.getBuilding(), building);
        }

        /// <summary>
        /// Tests adding a mountain building to a tile on the game board.
        /// </summary>
        [Fact]
        public void TestAddMountainBuildingToGameBoardWithBuilding()
        {
            // Arrange 
            GameBoard gameboard = new GameBoard(1, 1);
            gameboard.generateGameBoard();
            Tile tile = new Tile(1, 1);
            Building building = new Building(1.0f, new Resource(ResourceType.MOUNTAIN));
           
            // Act
            gameboard.addBuilding(building, tile);

            // Assert
            Assert.Equal(tile.getBuilding(), building);
        }

        /// <summary>
        /// Tests adding a forest building to a tile on the game board.
        /// </summary>
        [Fact]
        public void TestAddForestBuildingToGameBoardWithBuilding()
        {
            // Arrange 
            GameBoard gameboard = new GameBoard(1, 1);
            gameboard.generateGameBoard();
            Tile tile = new Tile(1, 1);
            Building building = new Building(1.0f, new Resource(ResourceType.FOREST));
            
            // Act
            gameboard.addBuilding(building, tile);
            
            // Assert 
            Assert.Equal(tile.getBuilding(), building);
        }

        /// <summary>
        /// Tests adding a grassland building to a tile on the game board.
        /// </summary>
        [Fact]
        public void TestAddGrasslandBuildingToGameBoardWithBuilding()
        {
            // Arrange
            GameBoard gameboard = new GameBoard(1, 1);
            gameboard.generateGameBoard();
            Tile tile = new Tile(1, 1);
            Building building = new Building(1.0f, new Resource(ResourceType.GRASSLANDS));
            
            // Act
            gameboard.addBuilding(building, tile);
            
            // Assert 
            Assert.Equal(tile.getBuilding(), building);
        }

        /// <summary>
        /// Tests adding a river building directly to a tile. 
        /// </summary>
        [Fact]
        public void TestAddRiverBuildingToTileBuildingValue()
        {
            // Arrange
            Tile tile = new Tile(1, 1);
            Building building = new Building(1.0f, new Resource(ResourceType.RIVER));
            
            // Act
            tile.addBuilding(building);
           
            // Assert
            Assert.Equal(tile.getBuilding(), building);
        }

        /// <summary>
        /// Tests adding a mountain building directly to a tile.
        /// </summary>
        [Fact]
        public void TestAddMountainBuildingToTileBuildingValue()
        {
            // Arrange 
            Tile tile = new Tile(1, 1);
            Building building = new Building(1.0f, new Resource(ResourceType.MOUNTAIN));
            
            // Act
            tile.addBuilding(building);
            
            // Assert 
            Assert.Equal(tile.getBuilding(), building);
        }

        /// <summary>
        /// Tests adding a forest building directly to a tile. 
        /// </summary>
        [Fact]
        public void TestAddForestBuildingToTileBuildingValue()
        {
            // Arrange
            Tile tile = new Tile(1, 1);
            Building building = new Building(1.0f, new Resource(ResourceType.FOREST));
            
            // Act
            tile.addBuilding(building);
            
            // Assert
            Assert.Equal(tile.getBuilding(), building);
        }

        /// <summary>
        /// Tests adding a grassland building directly to a tile.
        /// </summary>
        [Fact]
        public void TestAddGrasslandsBuildingToTileBuildingValue()
        {
            // Arrange
            Tile tile = new Tile(1, 1);
            Building building = new Building(1.0f, new Resource(ResourceType.GRASSLANDS));
            
            // Act
            tile.addBuilding(building);
            
            // Assert 
            Assert.Equal(tile.getBuilding(), building);
        }

        /// <summary>
        /// Tests adding no buidling to a tile. 
        /// </summary>
        [Fact]
        public void TestAddBuildingToTileNullValue()
        {
            // Arrange 
            Tile tile = new Tile(1, 1);

            // Assert 
            Assert.Null(tile.getBuilding());
        }

        /// <summary>
        /// Tests upgrading a building and verifying its level.
        /// </summary>
        [Fact]
        public void TestUpgradeBuilding()
        {
            // Arrange
            Building building = new Building(1.0f, new Resource(ResourceType.RIVER));
            
            // Act
            building.UpgradeBuilding();

            // Assert
            Assert.Equal(2, building.getLevel());
        }
    }
}
