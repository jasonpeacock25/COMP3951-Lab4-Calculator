using System.Collections;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

/// Namespace for our project.
/// 
/// Authors: Caleb, Tanner, Jason, Phyo
/// Date:    February 19, 2025
/// Version: 1.0
namespace Project
{
    /// <summary>
    /// Represents different types of resources available in the game.
    /// </summary>
    public enum ResourceType 
    { 
        RIVER      = 1,
        MOUNTAIN   = 2,
        GRASSLANDS = 3,
        FOREST     = 4
    }

    /// <summary>
    /// Represents a resource that can be gathered by buildings.
    /// </summary>
    public class Resource 
    { 
        private ResourceType type { get; }
        private float currentAmount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="type">The type of resource.</param>
        public Resource(ResourceType type) 
        { 
            this.type          = type;
            this.currentAmount = 0;
        }
    }

    /// <summary>
    /// Represents a building that can gather resources.
    /// </summary>
    public class Building 
    {
        private float resourceGatherRate { get; }
        private int level;
        private Resource resource { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building"/> class.
        /// </summary>
        /// <param name="resourceGatherRate">The rate at which the building gathers resources.</param>
        /// <param name="resource">The resource associated with the building.</param>
        public Building(float resourceGatherRate, Resource resource)
        {
            this.resourceGatherRate = resourceGatherRate;
            this.resource           = resource;
            this.level              = 1;
        }

        /// <summary>
        /// Gets the current level of the building.
        /// </summary>
        /// <returns>The level of the building.</returns>
        public int getLevel() 
        {
            return this.level;
        }

        /// <summary>
        /// Upgrades the building by increasing its level.
        /// </summary>
        public void UpgradeBuilding() 
        {
            this.level++;
        }
    }

    /// <summary>
    /// Represents the game board that contains tiles.
    /// </summary>
    public class GameBoard 
    {
        private int width { get; }
        private int height { get; }
        private List<Tile> tiles = new List<Tile>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoard"/> class.
        /// </summary>
        /// <param name="width">The width of the game board.</param>
        /// <param name="height">The height of the game board.</param>
        public GameBoard(int width, int height) 
        { 
            this.width  = width;
            this.height = height;
        }

        /// <summary>
        /// Gets the list of tiles on the game board.
        /// </summary>
        /// <returns>A list of tiles.</returns>
        public List<Tile> getTiles() 
        {
            return this.tiles;
        }

        /// <summary>
        /// Generates the game board by creating tiles.
        /// </summary>
        public void generateGameBoard() 
        {
            for (int i = 0; i < width; i++) 
            {
                for (int j = 0; j < height; j++) 
                {
                    Tile tile = new Tile(i, j);
                    this.tiles.Append(tile);
                }
            }
        }

        /// <summary>
        /// Adds a building to a specified tile.
        /// </summary>
        /// <param name="building">The building to add.</param>
        /// <param name="tile">The tile where the building is placed.</param>
        public void addBuilding(Building building, Tile tile) 
        {
            tile.addBuilding(building);
        }
    }

    /// <summary>
    /// Represents a tile on the game board.
    /// </summary>
    public class Tile 
    {
        private int x { get; }
        private int y { get; }
        private ResourceType resource;
        private Building? building;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class.
        /// </summary>
        /// <param name="x">The x-coordinate of the tile.</param>
        /// <param name="y">The y-coordinate of the tile.</param>
        public Tile(int x, int y) 
        {
            this.x        = x;
            this.y        = y;
            this.building = null;
        }

        /// <summary>
        /// Gets the building on this tile, if any.
        /// </summary>
        /// <returns>The building on this tile.</returns>
        public Building? getBuilding()
        {
            return this.building;
        }


        /// <summary>
        /// Adds a building to this tile.
        /// </summary>
        /// <param name="building">The building to add.</param>
        public void addBuilding(Building building) 
        { 
            this.building = building;
        }
    }
}
