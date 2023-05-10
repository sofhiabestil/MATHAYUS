using System.Collections.Generic;

public class QuestionManager
{
    private List<int> selectedIndex;

    public QuestionManager()
    {
        selectedIndex = new List<int>();
    }

    public void AddSelectedIndex(int index)
    {
        selectedIndex.Add(index);
    }

    public void ClearSelectedIndex()
    {
        selectedIndex.Clear();
    }

    public bool IsIndexSelected(int index)
    {
        return selectedIndex.Contains(index);
    }
}