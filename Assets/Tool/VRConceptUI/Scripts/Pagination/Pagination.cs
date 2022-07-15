using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epibyte.ConceptVR
{
    public class Pagination : MonoBehaviour
    {
        public Transform positions;
        public List<GameObject> items;
        int numberOfPositions;
        int numberOfPages;
        int currentPage = 0;
        bool isCleanedVacantCircles = false;
        Dictionary<int, List<PageItem>> pages = new Dictionary<int, List<PageItem>>();
        void Awake()
        {
            f_Reset();
        }

        public void f_Reset()
        {
            pages.Clear();
            numberOfPositions = positions.childCount;
            int page = 0;
            int posIdx = 0;
            int itemIdx = 0;

            List<GameObject> oData = new List<GameObject>();
            foreach (GameObject item in items)
            {
                GameObject go = Instantiate(item, positions.GetChild(posIdx));

                oData.Add(go);

                PageItem pageItem = go.AddComponent<PageItem>();
                pageItem.page = page;

                if (pages.ContainsKey(page))
                {
                    pages[page].Add(pageItem);
                }
                else
                {
                    pages[page] = new List<PageItem>() { pageItem };
                }


                if (page != 0)
                {
                    go.SetActive(false);
                }

                if (posIdx < numberOfPositions - 1)
                {
                    posIdx += 1;
                }
                else
                {
                    if (itemIdx < items.Count - 1)
                    {
                        page += 1;
                    }
                    posIdx = 0;
                }

                itemIdx++;
            }
            glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_MapObjInit, oData);
            numberOfPages = page;
            CleanUpVacantCircle();
            currentPage = 0;
        }

        public void Prev()
        {
            if (currentPage <= 0)
            {
                return;
            }

            foreach (PageItem item in pages[currentPage])
            {
                item.gameObject.SetActive(false);
            }

            currentPage -= 1;
            ResetCircle();

            foreach (PageItem item in pages[currentPage])
            {
                item.gameObject.SetActive(true);
            }
            CleanUpVacantCircle();
        }

        public void Next()
        {

            if (currentPage >= numberOfPages)
            {
                return;
            }

            foreach (PageItem item in pages[currentPage])
            {
                item.gameObject.SetActive(false);
            }

            currentPage += 1;
            ResetCircle();

            foreach (PageItem item in pages[currentPage])
            {
                item.gameObject.SetActive(true);
            }
            CleanUpVacantCircle();
        }


        void ResetCircle()
        {
            if (currentPage != numberOfPages && isCleanedVacantCircles)
            {
                for (int i = numberOfPositions - 1; i >= 0; i--)
                {
                    if (positions.GetChild(i).GetComponentInChildren<Circle>() == null) { continue; }
                    positions.GetChild(i).GetComponentInChildren<Circle>(true).gameObject.SetActive(true);
                }

                isCleanedVacantCircles = false;
            }
        }
        void CleanUpVacantCircle()
        {
            if (currentPage == numberOfPages)
            {
                if (items.Count % numberOfPositions == 0) { return; }

                int vacantCircles = numberOfPositions - items.Count % numberOfPositions;
                int circlesLeft = vacantCircles;

                for (int i = numberOfPositions - 1; i >= 0; i--)
                {
                    if(positions.GetChild(i).GetComponentInChildren<Circle>() == null) { continue; }
                    positions.GetChild(i).GetComponentInChildren<Circle>().gameObject.SetActive(false);
                    circlesLeft--;

                    if (circlesLeft == 0) { break; }
                }

                isCleanedVacantCircles = true;
            }
        }
    }
}