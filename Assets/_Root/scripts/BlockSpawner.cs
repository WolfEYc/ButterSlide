using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    Transform _transform;
    public Transform[] blocks;
    
    public float maxSize = 5f;
    public float minSize = 1f;
    public float minFrequency = 2f;
    public float maxFrequency = 4f;
    public float leftBound = -7f;
    public float rightBound = 7f;
    
    float _prevHeight;
    float _nextFrequency;
    int _blockIdx;

    Vector3 _blockScale = new(1f, 0.25f, 1f);

    bool AboveThreshold => _transform.position.y > _prevHeight + _nextFrequency;

    void Awake()
    {
        _transform = transform;
        ResetBlocks();
    }

    void MoveBlock()
    {
        var position = _transform.position;
        _prevHeight = position.y;
        _nextFrequency = Random.Range(minFrequency, maxFrequency);
        blocks[_blockIdx].position = new Vector3(Random.Range(leftBound, rightBound), position.y);
        _blockScale.x *= Random.Range(minSize, maxSize);
        blocks[_blockIdx].localScale = _blockScale;
        _blockScale.x = 1;
        _blockIdx++;
        if (_blockIdx >= blocks.Length)
        {
            _blockIdx = 0;
        }
    }

    public void ResetBlocks()
    {
        _prevHeight = _transform.position.y;
        _blockIdx = 0;
        _nextFrequency = Random.Range(minFrequency, maxFrequency);
        foreach (Transform block in blocks)
        {
            block.position = new Vector3(0, 0, -10);
        }
    }

    void Update()
    {
        if (AboveThreshold)
        {
            MoveBlock();
        }
    }
}
