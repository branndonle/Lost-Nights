using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MazeSpawner : MonoBehaviour
{
	public GameObject Floor = null;
	public GameObject Wall = null;
	public GameObject Pillar = null;
	public GameObject BigPowerup = null;
	public GameObject SpeedPowerup = null;

	public int Rows = 10;
	public int Columns = 10;
	public float CellWidth = 5;
	public float CellHeight = 5;
	public bool AddGaps = true;

	[SerializeField]
	private NavMeshSurface[,] navMeshSurfaces;

	private BasicMazeGenerator mMazeGenerator = null;

	void Awake()
	{
        navMeshSurfaces = new NavMeshSurface[Rows, Columns];
        mMazeGenerator = new TreeMazeGenerator(Rows, Columns);
		mMazeGenerator.GenerateMaze();
		for (int row = 0; row < Rows; row++)
		{
			for (int column = 0; column < Columns; column++)
			{
				float x = column * (CellWidth + (AddGaps ? .2f : 0));
				float z = row * (CellHeight + (AddGaps ? .2f : 0));
				MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
				GameObject tmp;
				NavMeshSurface nmsTmp;
				tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
				nmsTmp = tmp.AddComponent(typeof(NavMeshSurface)) as NavMeshSurface;
				navMeshSurfaces[row, column] = nmsTmp;
				tmp.transform.parent = transform;
				if (cell.WallRight)
				{
					tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;
					tmp.transform.parent = transform;
				}
				if (cell.WallFront)
				{
					tmp = Instantiate(Wall, new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
					tmp.transform.parent = transform;
				}
				if (cell.WallLeft)
				{
					tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;
					tmp.transform.parent = transform;
				}
				if (cell.WallBack)
				{
					tmp = Instantiate(Wall, new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;
					tmp.transform.parent = transform;
				}
			}
		}
		if (Pillar != null)
		{
			for (int row = 0; row < Rows + 1; row++)
			{
				for (int column = 0; column < Columns + 1; column++)
				{
					float x = column * (CellWidth + (AddGaps ? .2f : 0));
					float z = row * (CellHeight + (AddGaps ? .2f : 0));
					GameObject tmp = Instantiate(Pillar, new Vector3(x - CellWidth / 2, 0, z - CellHeight / 2), Quaternion.identity) as GameObject;
					tmp.transform.parent = transform;
				}
			}
		}
		for (int row = 0; row < Rows; row++)
		{
			for (int column = 0; column < Columns; column++)
			{
				NavMeshSurface nm = navMeshSurfaces[row, column];
				nm.BuildNavMesh();
			}
		}
		for (int i = 0; i < 5; i++)
        {
			// Create a new instance of the Random class.
			
			
			// Generate a random number between 0 (inclusive) and 10 (exclusive).
			GameObject tmpSpeedPowerup = Instantiate(SpeedPowerup, new Vector3(Random.Range(0, Rows) * (CellWidth + (AddGaps ? .2f : 0)), -4.5f, Random.Range(0, Columns) * (CellWidth + (AddGaps ? .2f : 0))), Quaternion.Euler(0, 0, 0)) as GameObject;
			tmpSpeedPowerup.gameObject.tag = "Speed";
			tmpSpeedPowerup.AddComponent<SphereCollider>();


		}

	}
}