using NUnit.Framework;
using UnityEngine;

namespace Tests.editMode
{
    public class canPlaceBuilding
    {
        [Test]
        public void CanPlaceBuilding_ReturnsTrue_WhenNoBuildingExistsAtGivenGridPosition() {
            // Arrange
            GameObject gameObject = new GameObject();
            GridManager gridManager = gameObject.AddComponent<GridManager>();
            gridManager.chunkGeneration = gameObject.AddComponent<ChunkGeneration>();
            gridManager.InitializeGrids();
            var building = ScriptableObject.CreateInstance<Building>();
            building.depth = 2;
            building.width = 2;
            int xGrid = 0;
            int yGrid = 0;

            // Act
            bool result = gridManager.canPlaceBuilding(xGrid, yGrid, building);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CanPlaceBuilding_ReturnsFalse_WhenBuildingExistsAtGivenGridPosition() {
            // Arrange
            GameObject gameObject = new GameObject();
            GridManager gridManager = gameObject.AddComponent<GridManager>();
            gridManager.chunkGeneration = gameObject.AddComponent<ChunkGeneration>();
            gridManager.InitializeGrids();
            var building = ScriptableObject.CreateInstance<Building>();
            building.depth = 2;
            building.width = 2;
            int xGrid = 0;
            int yGrid = 0;
            gridManager.topSetValue(xGrid, yGrid, new GameObject());

            // Act
            bool result = gridManager.canPlaceBuilding(xGrid, yGrid, building);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CanPlaceBuilding_ReturnsFalse_WhenBuildingExceedsGridBounds() {
            // Arrange
            GameObject gameObject = new GameObject();
            GridManager gridManager = gameObject.AddComponent<GridManager>();
            gridManager.chunkGeneration = gameObject.AddComponent<ChunkGeneration>();
            gridManager.InitializeGrids();
            var building = ScriptableObject.CreateInstance<Building>();
            building.depth = gridManager.chunkGeneration.chunkDepth + 1;
            building.width = gridManager.chunkGeneration.chunkWidth + 1;
            int xGrid = 0;
            int yGrid = 0;

            // Act
            bool result = gridManager.canPlaceBuilding(xGrid, yGrid, building);

            // Assert
            Assert.IsFalse(result);
        }
    }
}