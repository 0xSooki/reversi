using Model;

namespace ReversiMaui;

public class GameSizesViewModel : ViewModelBase
{
    private BoardSize _boardSize;

    public BoardSize Size
    {
        get => _boardSize;
        set
        {
            _boardSize = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(SizeText));
        }
    }

    public string SizeText => _boardSize.ToString();
}