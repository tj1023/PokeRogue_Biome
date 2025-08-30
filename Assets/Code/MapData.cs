using System.Collections.Generic;
using UnityEngine;

public static class MapData
{
    public class Edge
    {
        public readonly string Neighbor;
        public readonly float Probability;
        public Edge(string neighbor, float probability)
        {
            Neighbor = neighbor;
            Probability = probability;
        }
    }

    public class Node
    {
        public string Name;
        public Vector2 Position;
        public readonly List<Edge> Edges;
        public Node(string name, Vector2 position, List<Edge> edges)
        {
            Name = name;
            Position = position;
            Edges = edges;
        }
    }

    public static readonly Dictionary<string, Node> Nodes = new Dictionary<string, Node>()
    {
        // Position은 임시
        { "마을", new Node("마을", new Vector2(0,0), new List<Edge>{ new Edge("평야",1f) }) },
        { "평야", new Node("평야", new Vector2(1,1), new List<Edge>{ new Edge("대도시",1f), new Edge("풀숲",1f), new Edge("호수",1f) }) },
        { "대도시", new Node("대도시", new Vector2(0,1), new List<Edge>{ new Edge("슬럼",1f) }) },
        { "슬럼", new Node("슬럼", new Vector2(0,2), new List<Edge>{ new Edge("공사장",1f), new Edge("호수",0.5f) }) },
        { "공사장", new Node("공사장", new Vector2(2,4), new List<Edge>{ new Edge("발전소",1f), new Edge("도장",0.5f) }) },
        { "도장", new Node("도장", new Vector2(1,5), new List<Edge>{ new Edge("평야",1f), new Edge("정글",0.5f), new Edge("사원",0.5f) }) },
        { "발전소", new Node("발전소", new Vector2(0,3), new List<Edge>{ new Edge("공장",1f) }) },
        { "공장", new Node("공장", new Vector2(1,2), new List<Edge>{ new Edge("평야",1f), new Edge("연구소",0.5f) }) },
        { "연구소", new Node("연구소", new Vector2(2,3), new List<Edge>{ new Edge("공사장",1f) }) },
        { "풀숲", new Node("풀숲", new Vector2(1,0), new List<Edge>{ new Edge("높은풀숲",1f) }) },
        { "높은풀숲", new Node("높은풀숲", new Vector2(3,0), new List<Edge>{ new Edge("숲",1f), new Edge("동굴",1f) }) },
        { "숲", new Node("숲", new Vector2(3,2), new List<Edge>{ new Edge("정글",1f), new Edge("목초지",1f) }) },
        { "정글", new Node("정글", new Vector2(3,4.5f), new List<Edge>{ new Edge("사원",1f) }) },
        { "사원", new Node("사원", new Vector2(4,5), new List<Edge>{ new Edge("사막",1f), new Edge("늪지",0.5f), new Edge("고대유적",0.5f) }) },
        { "늪지", new Node("늪지", new Vector2(5,0.5f), new List<Edge>{ new Edge("높은풀숲",1f), new Edge("묘지",1f) }) },
        { "묘지", new Node("묘지", new Vector2(5,-1), new List<Edge>{ new Edge("심연",1f) }) },
        { "심연", new Node("심연", new Vector2(6,-1), new List<Edge>{ new Edge("동굴",1f), new Edge("우주",0.5f), new Edge("황무지",0.5f) }) },
        { "목초지", new Node("목초지", new Vector2(5,2), new List<Edge>{ new Edge("평야",1f), new Edge("페어리동굴",1f) }) },
        { "페어리동굴", new Node("페어리동굴", new Vector2(8,3), new List<Edge>{ new Edge("얼음동굴",1f), new Edge("우주",0.5f) }) },
        { "우주", new Node("우주", new Vector2(6,4), new List<Edge>{ new Edge("고대유적",1f) }) },
        { "고대유적", new Node("고대유적", new Vector2(5,4), new List<Edge>{ new Edge("산",1f), new Edge("숲",0.5f) }) },
        { "황무지", new Node("황무지", new Vector2(9,-1.5f), new List<Edge>{ new Edge("악지",1f) }) },
        { "악지", new Node("악지", new Vector2(7,-2), new List<Edge>{ new Edge("사막",1f), new Edge("산",1f) }) },
        { "사막", new Node("사막", new Vector2(4,4), new List<Edge>{ new Edge("고대유적",1f), new Edge("공사장",0.5f) }) },
        { "동굴", new Node("동굴", new Vector2(7,0), new List<Edge>{ new Edge("호수",1f), new Edge("악지",1f), new Edge("연구소",0.5f) }) },
        { "호수", new Node("호수", new Vector2(7,1), new List<Edge>{ new Edge("늪지",1f), new Edge("해변",1f) }) },
        { "해변", new Node("해변", new Vector2(8,1), new List<Edge>{ new Edge("바다",1f), new Edge("섬",0.5f) }) },
        { "섬", new Node("섬", new Vector2(8,0), new List<Edge>{ new Edge("바다",1f) }) },
        { "바다", new Node("바다", new Vector2(9,0), new List<Edge>{ new Edge("해저",1f), new Edge("얼음동굴",1f) }) },
        { "해저", new Node("해저", new Vector2(8,-1), new List<Edge>{ new Edge("동굴",1f), new Edge("화산",0.33f) }) },
        { "산", new Node("산", new Vector2(11,1), new List<Edge>{ new Edge("화산",1f), new Edge("황무지",0.5f), new Edge("우주",0.33f) }) },
        { "화산", new Node("화산", new Vector2(10,1), new List<Edge>{ new Edge("해변",1f), new Edge("얼음동굴",0.33f) }) },
        { "얼음동굴", new Node("얼음동굴", new Vector2(9,3), new List<Edge>{ new Edge("눈덮인숲",1f) }) },
        { "눈덮인숲", new Node("눈덮인숲", new Vector2(8,2), new List<Edge>{ new Edge("산",0.5f), new Edge("호수",0.5f) }) },
    };
}
