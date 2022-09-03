using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapRoutine", menuName = "��/������ƾ")]
public class MapRoutine : ScriptableObject
{
    [Header("�����̸����")]
    public string[] SolarTerm;
    [Header("�� ����,����")]
    public int[] mapWidth, mapHeight;//�� ���� ����
    [Header("�ϸ��� ����")]
    public int[] minSectionWidth;//�ϸ��� ����
    [Header("�������� Ȯ��")]
    public int[] cliff_Pro;
    [Header("�������� �ִ� ũ��")]
    public int[] maxCliffGap;
}
