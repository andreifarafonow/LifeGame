using GameCore;
using GameCore.GameEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LifeGame
{
    //review: Избегай статических классов. Сделай обычный и положи в DI.
    //Слова Displayer не то чтобы нету в английском языке, но оно обычно не используется. 
    //Можно было бы сделать так - BaseGamePresentation и унаследованный от него ConsoleGamePresentation
    //(держим в голове, что мы можем захотеть отображать поле как угодно, а консоль - только для отладки)
    //Но у нас отображение, например, на вебе, будет работать совсем по другому (и мы еще точно не знаем как) - т.к. там не мы рулим циклом работы приложения из Main(),
    //а фреймворк дергает наши методы. Поэтому иерархия здесь будет неуместна.
    //Поэтому я бы просто назвал его ConsoleGamePresentation. Если что-то потребуется потом вытянуть в базовый класс (если он будет) - отрефакторим по мере появления потребности.
    static class GameDisplayer
    {
        static int gridLeftMargin = 4, gridTopMargin = 2, 
                   cellWidth = 9, cellHeight = 5;

        static Dictionary<WorldCell.CellType, ConsoleColor> cellColors = new Dictionary<WorldCell.CellType, ConsoleColor>()
        {
            { WorldCell.CellType.Ground,  ConsoleColor.DarkGreen },
            { WorldCell.CellType.Water,  ConsoleColor.Cyan }
        };


        //review: лучше сделать класс Coordinates {int X {get; set;}, int Y {get; set;}}
        //у тебя много где передаются x и y, сразу просится класс для их объединения.
        //В нем потом наверняка пригодятся методы для сравнения
        //UPD: А у нас же есть Point для этого. ВОт его и передавай.
        //Чем меньше аргументов у метода, тем лучше.
        static void DrawCellBackground(int x, int y, WorldCell.CellType cellType)
        {
            for (int i = 0; i < cellHeight; i++)
            {
                //review: Любое обращение по [] опасно, т.к. может привести к OutOfRangeException
                //Если ты обращаешься к массиву или словарю не там же, где ты его только что заполнил, у тебя нет гарантии, что элемент с этим индексом там есть
                //Поэтому лучше это обернуть в метод, который выбросит осмысленное исключение.
                //Я бы сделал extension-метод ConsoleColor GetConsoleColor(this WorldCell.CellType celltype)
                //Это нормально, когда к объекту, ничего не знающему о данном уровне абстракции (WorldCell не знает про консоль), добавляют extension-методы из этого уровня.
                Console.ForegroundColor = Console.BackgroundColor = cellColors[cellType];

                Console.SetCursorPosition(gridLeftMargin + x * cellWidth, gridTopMargin + y * cellHeight + i);

                Console.WriteLine(string.Concat(Enumerable.Repeat('█', cellWidth)));
            }
        }

        static void DrawObjectsNumInCell(int x, int y, int num)
        {
            Console.SetCursorPosition(gridLeftMargin + x * cellWidth + 3, gridTopMargin + y * cellHeight + 2);
            Console.Write($"[{num}]");
        }

        static void DrawObjectsNamesInCell(int x, int y, IEnumerable<GameObject> objects)
        {
            int offset = 0;

            foreach (var obj in objects)
            {
                string name = obj.ToString();
                Console.SetCursorPosition(gridLeftMargin + obj.Position.X * cellWidth + 4 - name.Length / 2, gridTopMargin + obj.Position.Y * cellHeight + 2 - offset);
                Console.Write(name);

                if (offset <= 0)
                    offset = -offset + 1;
                else
                    offset = -offset;
            }
        }

        public static void Display(Game game)
        {
            for (int y = 0; y < game.Map.Size.Height; y++)
            {
                for (int x = 0; x < game.Map.Size.Width; x++)
                {
                    DrawCellBackground(x, y, game.Map[y, x].TypeOfCell);

                    var objectsOnThisCell = game.GameObjects.Where(obj => obj.Position == new Point(x, y));

                    Console.ForegroundColor = ConsoleColor.Black;

                    if (objectsOnThisCell.Count() > 3)
                    {
                        DrawObjectsNumInCell(x, y, objectsOnThisCell.Count());
                    }
                    else if (objectsOnThisCell.Count() > 0)
                    {
                        DrawObjectsNamesInCell(x, y, objectsOnThisCell);
                    }
                }
            }

            Console.ResetColor();

            Console.SetCursorPosition(0, game.Map.Size.Height * cellHeight + gridTopMargin + 1);
        }
    }
}
