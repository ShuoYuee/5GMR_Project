/* Store the contents for ListBoxes to display.
 */
using UnityEngine;

/* The base class of the list content container
 *
 * Create the individual ListBank by inheriting this class
 */
public abstract class ListBankBase: MonoBehaviour
{
	public abstract object GetListContent(int index);
	public abstract int GetListLength();
}
