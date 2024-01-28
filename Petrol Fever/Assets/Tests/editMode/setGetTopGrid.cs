using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class setGetTopGrid
{
    [Test]
public void TopSetValue_SetsValue_WhenIndicesAreValid() {
    // Arrange
    GameObject gameObject = new GameObject();
    GridManager gridManager = gameObject.AddComponent<GridManager>();
    gridManager.chunkGeneration = gameObject.AddComponent<ChunkGeneration>();
    gridManager.InitializeGrids();
    GameObject building = new GameObject();
    int x = 0;
    int z = 0;

    // Act
    gridManager.topSetValue(x, z, building);

    // Assert
    Assert.AreEqual(building, gridManager.topGetBuilging(x, z));
}

[Test]
public void TopSetValue_DoesNotSetValue_WhenIndicesAreInvalid() {
    // Arrange
    GameObject gameObject = new GameObject();
    GridManager gridManager = gameObject.AddComponent<GridManager>();
    gridManager.chunkGeneration = gameObject.AddComponent<ChunkGeneration>();
    gridManager.InitializeGrids();
    GameObject building = new GameObject();
    int x = -1;
    int z = -1;

    // Act
    gridManager.topSetValue(x, z, building);

    // Assert
    Assert.IsNull(gridManager.topGetBuilging(x, z));
}

[Test]
public void TopGetBuilding_ReturnsBuilding_WhenIndicesAreValid() {
    // Arrange
    GameObject gameObject = new GameObject();
    GridManager gridManager = gameObject.AddComponent<GridManager>();
    gridManager.chunkGeneration = gameObject.AddComponent<ChunkGeneration>();
    gridManager.InitializeGrids();
    GameObject building = new GameObject();
    int x = 0;
    int z = 0;
    gridManager.topSetValue(x, z, building);

    // Act
    var result = gridManager.topGetBuilging(x, z);

    // Assert
    Assert.AreEqual(building, result);
}

[Test]
public void TopGetBuilding_ReturnsNull_WhenIndicesAreInvalid() {
    // Arrange
    GameObject gameObject = new GameObject();
    GridManager gridManager = gameObject.AddComponent<GridManager>();
    gridManager.chunkGeneration = gameObject.AddComponent<ChunkGeneration>();
    gridManager.InitializeGrids();
    int x = -1;
    int z = -1;

    // Act
    var result = gridManager.topGetBuilging(x, z);

    // Assert
    Assert.IsNull(result);
}
}
