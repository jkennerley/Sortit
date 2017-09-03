using System.Collections.Generic;
using System.Linq;

namespace Sortit
{
    public static class SortitExtensions
    {
        public static IOrderedQueryable<T> Sortit<T>(this IQueryable<T> queryable, List<SorterMeta> sorterMetas)
        {
            // swap to a List
            //var sorterMetas =
            //    isortersMetas
            //    //.ToList()
            //    ;

            // Send the queryable parameter to the SortitFirst,
            // SortitFirst() takes an IQueryable argument
            // SortitFirst() returns an IOrderedQueryable
            // this is assigned to the return value of this function
            var orderedQueryableRet =
                queryable
                .SortitFirst(
                    sorterMetas[0]
                );

            // Except for the first sorterMetas, i.e. sorterMetas[0]
            // send each sorterMeta of the sorterMetas parameter
            // Send the return value of this function to Sortit(), passing in each sorterMeta
            // and then assign the return value to the return value of this function i.e. orderedQueryableRet
            // Sortit(), returns an IOrderedQueryable
            sorterMetas
                .Skip(1)
                .ToList()
                .ForEach(sorterMeta =>
                    orderedQueryableRet = orderedQueryableRet.Sortit(sorterMeta)
                );

            // Return
            return orderedQueryableRet;
        }

        public static IOrderedQueryable<T> SortitFirst<T>(this IQueryable<T> queryable, SorterMeta sorterMeta)
        {
            if (sorterMeta.Desc)
            {
                return queryable.OrderByDescending(sorterMeta.PropertyName);
            }
            else
            {
                return queryable.OrderBy(sorterMeta.PropertyName);
            }
        }

        public static IOrderedQueryable<T> Sortit<T>(this IOrderedQueryable<T> queryable, SorterMeta sorterMeta)
        {
            if (sorterMeta.Desc)
            {
                return queryable.ThenByDescending(sorterMeta.PropertyName);
            }
            else
            {
                return queryable.ThenBy(sorterMeta.PropertyName);
            }
        }

        public static List<SorterMeta> ToSorterMetas(string sorterMetaString)
        {
            // maps
            //        "Surname, Forename desc"
            //     -> ["Surname , "Forename desc"]
            var sorterMetaItemStrings =
                    sorterMetaString
                    .Trim()
                    .Split(',');

            // maps
            //       ["Surname , "Forename desc"]
            //    -> [ ["Surname ], ["Forename", "desc"]]
            var sorterMetaItems =
                sorterMetaItemStrings
                    .Select(x =>
                        x.Trim().Split(' ')
                    );

            // maps
            //           [ ["Surname ], ["Forename", "desc"]]
            // List<> -> {
            //              { "Surname" , Desc = false }
            //             ,{ "Forname" , Desc = true }
            //          }
            var sorterMetas =
                sorterMetaItems
                    .Select(sorterMetaItem =>
                    {
                        // sorterMetaItem, is something like ...
                        // e.g. [ ] //  empty
                        // e.g. ["Surname" ] // array with 1 element
                        // e.g. ["Surname" , "desc"] // array with 2 elements
                        switch (sorterMetaItem.Length)
                        {
                            case 0:
                                //return new SorterMeta { PropertyName = string.Empty };
                                return null;

                            case 1:
                                return new SorterMeta { PropertyName = sorterMetaItem[0], Desc = false };

                            case 2:
                                var propertyName = sorterMetaItem[0];
                                var desc = sorterMetaItem[1].ToLowerInvariant().Contains("desc");
                                return new SorterMeta { PropertyName = propertyName, Desc = desc };

                            default:
                                // Refactor : should probably cause exception here !
                                return null;
                        }
                    });

            //var sorterMetasRet =sorterMetas.ToList();

            // Return
            return sorterMetas.ToList();
        }
    }
}