using UnityEngine;

public class ArrowsSpawner : MonoBehaviour
{
    public int index;
    public int spawned;
    public int count;
    
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Animator door;
    [SerializeField] private Direction[] directions;
    [SerializeField] private float distance;

    private bool _opened;
    
    private enum Direction
    {
        Up = 270, Left = 180, Right = 0, Down = 90
    }

    private void Start()
    {
        count = directions.Length;
    }

    public void ResetColliders()
    {
        if (_opened) return;
        transform.GetChild(0).GetComponent<Collider>().enabled = true;
    }
    
    public void Spawn(Vector3 position)
    {
        if (index == count)
        {
            var start = transform.GetChild(0).GetComponent<ArrowCollision>();
            start.GetComponent<Collider>().enabled = false;
            _opened = true;
            StartCoroutine(start.Die());
            
            door.Play("Open Door", -1, 0f);
            return;
        }

        var pos = position;
        var rot = Quaternion.Euler(0f, 90f, 0f);
        
        if (directions[index] == Direction.Left)
            pos += Vector3.left * distance;
        else if (directions[index] == Direction.Right)
            pos += Vector3.right * distance;
        else if (directions[index] == Direction.Down)
            pos += Vector3.down * distance;
        else if (directions[index] == Direction.Up)
            pos += Vector3.up * distance;

        if (index == count - 1)
        {
            var keySphere = Instantiate(key, pos, rot, transform);
            StartCoroutine(keySphere.GetComponent<ArrowCollision>().Die());
            index++;
            spawned++;
            return;
        }
        
        rot = Quaternion.Euler((float) directions[index + 1], 90f, 0f);
        
        var arrowInstance = Instantiate(arrow, pos, rot, transform);
        StartCoroutine(arrowInstance.GetComponent<ArrowCollision>().Die());
         
        spawned++;
        index++;
    }
}
