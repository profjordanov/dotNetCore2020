using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pluralsight.AdvCShColls.TourBooker.Logic
{
    public class ChangeUndoer
    {
        public static void Undo(LinkedList<Country> itinerary, ItineraryChange changeToUndo)
        {
            switch (changeToUndo.ChangeType)
            {
                case ChangeType.Append:
                    // change was to append final country - so we need to remove it.
                    itinerary.RemoveLast();
                    break;
                case ChangeType.Insert:
					// change was to insert a country before item X. So remove the country before X
                    LinkedListNode<Country> insertion = itinerary.GetNthNode(changeToUndo.Index);
                    itinerary.Remove(insertion);
                    break;
                case ChangeType.Remove:
                    // change was to remove a country before node X. So need to insert it back

                    // if country removed was the last one in the list...
                    if (changeToUndo.Index == itinerary.Count)
                    {
                        itinerary.AddLast(changeToUndo.Value);
                    }
                    else
                    {
                        LinkedListNode<Country> removedBefore = itinerary.GetNthNode(changeToUndo.Index);
                        itinerary.AddBefore(removedBefore, changeToUndo.Value);
                    }
                    break;
            }
        }
    }
}
