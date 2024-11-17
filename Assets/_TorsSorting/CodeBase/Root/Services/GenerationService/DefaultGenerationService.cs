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

        private void GenerateByString(ref PinData[] pins, int pinsCount, ref List<int> colors)
        {
            for (int i = 0; i < pinsCount; i++)
                pins[i].Initialize(Const.TorsInPin);


            for (int j = 0; j < Const.TorsInPin; j++)
            {
                for (int i = 0; i < pinsCount; i++)
                {
                    int randomColor = 0;
                    if (colors.Count > 0)
                    {
                        randomColor = colors[Random.Range(0, colors.Count)];
                        colors.Remove(randomColor);
                    }
                    
                    pins[i].TorsColors[j] = randomColor;
                }
            }
        }

        private void GenerateFillPin(ref PinData[] pins, int pinsCount, ref List<int> colors)
        {
            if (pinsCount == 2)
            {
                GenerateByString(ref pins, pinsCount, ref colors);
                return;
            }
            
            for (int i = 0; i < pinsCount; i++)
                pins[i].Initialize(Const.TorsInPin);

            for (int i = 0; i < pinsCount; i++)
            {
                int identityColors = 1;
                for (int j = 0; j < pins[i].TorsColors.Length; j++)
                {
                    int randomColor;
                    if (colors.Count > 0)
                        randomColor = colors[Random.Range(0, colors.Count)];
                    else 
                        return;

                    if (j > 0) //check identity colors
                    {
                        if (pins[i].TorsColors[j - 1] == randomColor)
                            identityColors++;

                        if (identityColors == pins[i].TorsColors.Length)
                        {
                            if (colors.Count > 0)
                            {
                                while (randomColor == pins[i].TorsColors[j - 1])
                                {
                                    randomColor = colors[Random.Range(0, colors.Count)];
                                }
                            }
                            else
                            {
                                pins[pinsCount].Initialize(Const.TorsInPin);
                                pins[pinsCount].TorsColors[0] = colors[0];
                                colors.RemoveAt(0);
                            }
                        }
                    }
                    
                    pins[i].TorsColors[j] = randomColor;
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