using PokemonSwitch.Interface;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static PokemonSwitch.FinishedPopupPage;

namespace PokemonSwitch
{
    public class Map : ContentPage, IMapDelegate
    {
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };
        Grid controlGrid = new Grid { RowSpacing = 1, ColumnSpacing = 1 };
        Grid mainGrid = new Grid { RowSpacing = 1, ColumnSpacing = 1 };
        Style plainButton, orangeButton;
        bool[] arrStyle = { true, true, false, false, true, false, false, false, false, false, false, false, false, false, false, false };
        FinishedPopupPage finishPage;
        MapSaver mapSaved = new MapSaver();
        Dictionary<int, List<MapSaver>> dicStepToMap = new Dictionary<int, List<MapSaver>>();
        int currentLevel, currentMapIndex, currentStep;
        HeaderVM _headerVM = new HeaderVM();
        public Map()
        {
            currentLevel = 2;
            currentMapIndex = 0;
            finishPage = new FinishedPopupPage(5);
            finishPage.SetMapDelegate(this);
            Title = "Calculator - C#";
            BackgroundColor = Color.FromHex("#404040");
            #region Button Style
            plainButton = new Style(typeof(ImageButton))
            {
                Setters = {
      new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex ("#eee") },
      new Setter { Property = Button.TextColorProperty, Value = Color.Black },
      new Setter { Property = Button.BorderRadiusProperty, Value = 5 },
      new Setter { Property = Button.FontSizeProperty, Value = 40 },
      new Setter {Property = ImageButton.OrientationProperty , Value = ImageOrientation.ImageCentered}
    }
            };
            var darkerButton = new Style(typeof(Button))
            {
                Setters = {
      new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex ("#ddd") },
      new Setter { Property = Button.TextColorProperty, Value = Color.Black },
      new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
      new Setter { Property = Button.FontSizeProperty, Value = 40 }
    }
            };
            orangeButton = new Style(typeof(Button))
            {
                Setters = {
      new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex ("#E8AD00") },
      new Setter { Property = Button.TextColorProperty, Value = Color.White },
      new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
      new Setter { Property = Button.FontSizeProperty, Value = 40 },
      new Setter {Property = ImageButton.OrientationProperty , Value = ImageOrientation.ImageCentered}
    }
            };
            #endregion

            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            Header header = new Header();
            mainGrid.Children.Add(header, 0, 0);
            Grid.SetRowSpan(header, 2);
            header.BindingContext = _headerVM;

            controlGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 0, 0);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 0, 1);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 0, 2);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 0, 3);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 1, 0);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 1, 1);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 1, 2);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 1, 3);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 2, 0);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 2, 1);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 2, 2);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 2, 3);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 3, 0);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 3, 1);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 3, 2);
            controlGrid.Children.Add(new ImageButton { Style = plainButton }, 3, 3);

            mainGrid.Children.Add(controlGrid, 0, 2);
            Grid.SetRowSpan(controlGrid, 4);

            MapFooter footer = new MapFooter(this);
            mainGrid.Children.Add(footer, 0, 6);
            Grid.SetColumnSpan(footer, 1);

            mainGrid.BackgroundColor = Color.FromHex("#558B2F");
            Content = mainGrid;

        }

        async void UpdateButton(int index, bool useRotateAnimation = true)
        {
            ImageButton buttonI = (ImageButton)controlGrid.Children[index];
            if(useRotateAnimation)
                await buttonI.RotateXTo(180);

                if (arrStyle[index])
            {
                buttonI.Style = orangeButton;
                buttonI.Source = "Charizard.png";
            }
            else
            {
                buttonI.Style = plainButton;
                buttonI.Source = "Venusaur.png";
            }

            if(useRotateAnimation)
                await buttonI.RotateXTo(0);
        }

        bool IsSolved()
        {
            bool checkVal = arrStyle[0];
            for (int i = 1; i < 16; i++)
            {
                if (arrStyle[i] != checkVal) return false;
            }
            return true;
        }
        private int ConvertStepToRating(int minStep)
        {
            double completePercent = (double)((double)minStep / (double)currentStep) * 100;
            if (completePercent <= 33) return 1;
            else if (completePercent <= 66) return 2;
            else return 3;

        }

        private async void ButtonI_Clicked(object sender, EventArgs e)
        {
            currentStep++;
            int index = Int32.Parse(((View)sender).ClassId);
            arrStyle[index] = !arrStyle[index];
            UpdateButton(index, true);
            for (int i = 0; i < 4; i++)
            {
                int nextIndexX = index / 4 + dx[i];
                int nextIndexY = index % 4 + dy[i];
                if (nextIndexX >= 0 && nextIndexX < 4 && nextIndexY >= 0 && nextIndexY < 4)
                {
                    int nextIndex = nextIndexX * 4 + nextIndexY;
                    arrStyle[nextIndex] = !arrStyle[nextIndex];
                    UpdateButton(nextIndex, true);
                }
            }
            if (IsSolved())
            {
                finishPage.SetStar(ConvertStepToRating(dicStepToMap[currentLevel][currentMapIndex].nSolvedStep));
                await PopupNavigation.PushAsync(finishPage);
            }

        }
        private void SolveAndSave()
        {
            Algorithm algo = new Algorithm();
            List<Node> listRes = algo.Solve(arrStyle);
            mapSaved = new MapSaver(listRes.Count, arrStyle, listRes);
        }
        private string LevelIntToText(int level)
        {
            string strLevel = "";
            switch(level)
            {
                case 2:
                    strLevel = "Very Easy"; break;
                case 3:
                    strLevel = "Easy"; break;
                case 4:
                    strLevel = "Hard"; break;
                case 5:
                    strLevel = "Very Hard"; break;
                default:
                    strLevel = "Extra Hard"; break;
            }
            return strLevel;
        }
        public async void SetMap(Dictionary<int, List<MapSaver>> dic, int level)
        {
            currentStep = 0;
            dicStepToMap = new Dictionary<int, List<MapSaver>>(dic);
            currentLevel = level;
            currentMapIndex = 0;
            _headerVM.Level = LevelIntToText(level);
            _headerVM.Gate = (currentMapIndex + 1) + "/" + dicStepToMap[currentLevel].Count;
            _headerVM.BestSolve = level + " Steps";
            await controlGrid.ScaleTo(0);

            for (int i = 0; i < 16; i++)
            {
                arrStyle[i] = dicStepToMap[currentLevel][currentMapIndex].arrStyle[i];
                ImageButton buttonI = (ImageButton)controlGrid.Children[i];
                controlGrid.Children[i].ClassId = i.ToString();
                UpdateButton(i, false);
                buttonI.Clicked += ButtonI_Clicked;
            }

            await controlGrid.ScaleTo(1);

        }
        public async void NextMap(PopupChoosen choosen)
        {
            currentStep = 0;
            if (choosen == PopupChoosen.NextMap)
            {
                if (currentMapIndex == dicStepToMap[currentLevel].Count - 1)
                {
                    currentLevel++;
                    currentMapIndex = 0;
                }
                else
                    currentMapIndex++;
            }

            _headerVM.Level = LevelIntToText(currentLevel);
            _headerVM.Gate = (currentMapIndex + 1) + "/" + dicStepToMap[currentLevel].Count;
            _headerVM.BestSolve = currentLevel + " Steps";
            await controlGrid.ScaleTo(0.0);

            for (int i = 0; i < 16; i++)
            {
                if (arrStyle[i] != dicStepToMap[currentLevel][currentMapIndex].arrStyle[i])
                {
                    arrStyle[i] = dicStepToMap[currentLevel][currentMapIndex].arrStyle[i]; ;
                    UpdateButton(i, false);
                }

            }

            await controlGrid.ScaleTo(1);
        }

        public async void TipForUser()
        {
            Algorithm algo = new Algorithm();
            List<Node> listRes = algo.Solve(arrStyle);
            MapSaver mSaver = new MapSaver(listRes.Count, arrStyle, listRes);
            if (listRes.Count > 0)
            {
                Node firstSolveNode = listRes[0];
                arrStyle[firstSolveNode.index] = !arrStyle[firstSolveNode.index];
                UpdateButton(firstSolveNode.index, true);
                for (int i = 0; i < 4; i++)
                {
                    int nextIndexX = firstSolveNode.index / 4 + dx[i];
                    int nextIndexY = firstSolveNode.index % 4 + dy[i];
                    if (nextIndexX >= 0 && nextIndexX < 4 && nextIndexY >= 0 && nextIndexY < 4)
                    {
                        int nextIndex = nextIndexX * 4 + nextIndexY;
                        arrStyle[nextIndex] = !arrStyle[nextIndex];
                        UpdateButton(nextIndex, true);
                    }
                }
                DependencyService.Get<IToastAndViberate>().ShowToast("Pressed index " + firstSolveNode.index + ". There are " + (listRes.Count - 1) + " step(s) to solve.");
                DependencyService.Get<IToastAndViberate>().Vibration(200);
            }

            if (IsSolved())
            {
                finishPage.SetStar(ConvertStepToRating(dicStepToMap[currentLevel][currentMapIndex].nSolvedStep));
                await PopupNavigation.PushAsync(finishPage);
            }
        }
    }

    public class MapSaver
    {
        public int nSolvedStep;
        public bool[] arrStyle;
        public List<Node> listResult;
        public MapSaver() { }
        public MapSaver(int _nStep, bool [] arr, List<Node> l)
        {
            if (arrStyle == null) arrStyle = new bool[16];
            nSolvedStep = _nStep;
            for (int i = 0; i < arr.Length; i++)
                arrStyle[i] = arr[i];
            listResult = new List<Node>(l);
        }
    }
}
