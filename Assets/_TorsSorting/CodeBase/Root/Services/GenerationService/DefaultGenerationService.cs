using System;
using System.Collections.Generic;
using CodeBase.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Root.Services
{
    public class DefaultGenerationService : IGenerationService
    {
        public PinData[] Generate(int levelDifficulty) //0 - tutorial
        {
            PinData[] generatedMap = new PinData[9];
            int totalPins = Const.BasePinsCount + levelDifficulty;
            int totalColors = totalPins - 1;
            
            List<int> colors = new();
            for (int i = 1; i <= totalColors; i++)
            {
                colors.Add(i);
                colors.Add(i);
                colors.Add(i);
            }
            
            GenerationType generationType = GetGenerationType();
            switch (generationType)
            {
                case GenerationType.ByTurns:
                    GenerateByString(ref generatedMap, totalPins, ref colors);
                    break;
                case GenerationType.FillPin:
                    GenerateFillPin(ref generatedMap, totalPins, ref colors);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return generatedMap;
        }

        private void GenerateByString(ref PinData[] map, int pinsCount, ref List<int> colors)
        {
            for (int i = 0; i < pinsCount; i++)
                map[i].Initialize(Const.TorsInPin);

            int iterations = 0;
            while (true)
            {
                for (int i = 0; i < pinsCount; i++)
                {
                    int randomColor;
                    if (colors.Count > 0)
                        randomColor = colors[Random.Range(0, colors.Count)];
                    else
                        return;
                    
                    map[i].TorsColors[iterations] = randomColor;
                    //Debug.Log(randomColor);
                    colors.Remove(randomColor);
                }
                iterations++;
            }
        }

        private void GenerateFillPin(ref PinData[] map, int pinsCount, ref List<int> colors)
        {
            if (pinsCount == 2)
            {
                GenerateByString(ref map, pinsCount, ref colors);
                return;
            }
            
            for (int i = 0; i < pinsCount; i++)
                map[i].Initialize(Const.TorsInPin);

            for (int i = 0; i < pinsCount; i++)
            {
                int identityColors = 1;
                for (int j = 0; j < map[i].TorsColors.Length; j++)
                {
                    int randomColor;
                    if (colors.Count > 0)
                        randomColor = colors[Random.Range(0, colors.Count)];
                    else 
                        return;

                    if (j > 0) //check identity colors
                    {
                        if (map[i].TorsColors[j - 1] == randomColor)
                            identityColors++;

                        if (identityColors == map[i].TorsColors.Length)
                        {
                            if (colors.Count > 0)
                            {
                                while (randomColor == map[i].TorsColors[j - 1])
                                {
                                    randomColor = colors[Random.Range(0, colors.Count)];
                                }
                            }
                            else
                            {
                                map[pinsCount].Initialize(Const.TorsInPin);
                                map[pinsCount].TorsColors[0] = colors[0];
                                colors.RemoveAt(0);
                            }
                        }
                    }
                    
                    map[i].TorsColors[j] = randomColor;
                    //Debug.Log(randomColor);
                    colors.Remove(randomColor);
                }
            }
        }

        private GenerationType GetGenerationType()
        {
            int i = Random.Range(0, 2); //0 or 1

            return i == 0 ? GenerationType.ByTurns : GenerationType.FillPin;
        }

        enum GenerationType
        {
            ByTurns,
            FillPin
        }
    }
}