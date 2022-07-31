using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapRoutine", menuName = "맵/계절루틴")]
public class MapRoutine : ScriptableObject
{
    [Header("절기이름목록")]
    public string[] SolarTerm;
    [Header("맵 가로,세로")]
    public int[] mapWidth, mapHeight;//맵 가로 세로
    [Header("완만한 정도")]
    public int[] minSectionWidth;//완만한 정도
    [Header("낭떠러지 확률")]
    public int[] cliff_Pro;
    [Header("낭떠러지 최대 크기")]
    public int[] maxCliffGap;
}
