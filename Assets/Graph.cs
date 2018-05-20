using UnityEngine;

public class Graph : MonoBehaviour
{

    public Transform pointPrefab;
    [Range(10,100)]
    public int resolution = 10;


    public enum GraphFunctionName { Sine, MultiSine };
    public GraphFunctionName function;
    
    Transform[] points;

    static float SineFunction(float x, float t)
    {
       return Mathf.Sin(Mathf.PI * (x + t));
    }

    static float MultiSineFunction(float x, float t)
    {   
        float y = Mathf.Sin(Mathf.PI * (x + t));
        y += Mathf.Sin(2f*Mathf.PI * (x + 2f*t))/2f;
        y *= 2f / 3f;
        return y;
    }

    void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one*step;
        Vector3 position;
        points = new Transform[resolution];
        
        for (int i=0;i<resolution;i++) {
            Transform point = Instantiate(pointPrefab);
            position.x = (i+0.5f)*step-1f;
            position.z = 0f;
            position.y = 0;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }
    private void Update()
    {
        float t = Time.time;
        
        GraphFunction[] functions= {SineFunction,MultiSineFunction};
        GraphFunction f=functions[(int)function];
        for (int i=0;i<points.Length;++i)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = f(position.x, t);
            point.localPosition = position;
        }
    }

}