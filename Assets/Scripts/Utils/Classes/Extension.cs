using System.Collections.Generic;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Utils.Classes
{
    public static class Vector2Extension
    {
        /// <summary>
        /// sustrac a value to the vector's x
        /// </summary>
        /// <param name="caller">vector</param>
        /// <param name="val">the value to substrack</param>
        /// <returns>the vector with good x</returns>
        public static Vector2 SubX(this Vector2 caller, float val)
        {
            return caller + Vector2.left * val;
        }

        /// <summary>
        /// sustrac a value to the vector's y
        /// </summary>
        /// <param name="caller">vector</param>
        /// <param name="val">the value to substrack</param>
        /// <returns>the vector with good y</returns>
        public static Vector2 SubY(this Vector2 caller, float val)
        {
            return caller + Vector2.down * val;
        }

        /// <summary>
        /// addition a value to the vector's x
        /// </summary>
        /// <param name="caller">vector</param>
        /// <param name="val">the value to addition</param>
        /// <returns>the vector with good x</returns>
        public static Vector2 SumX(this Vector2 caller, float val)
        {
            return caller + Vector2.right * val;
        }

        /// <summary>
        /// addition a value to the vector's y
        /// </summary>
        /// <param name="caller">vector</param>
        /// <param name="val">the value to addition</param>
        /// <returns>the vector with good y</returns>
        public static Vector2 SumY(this Vector2 caller, float val)
        {
            return caller + Vector2.up * val;
        }
    }

    public static class Vector3Extension
    {
        /// <summary>
        /// sustrac a value to the vector's x
        /// </summary>
        /// <param name="caller">vector</param>
        /// <param name="val">the value to substrack</param>
        /// <returns>the vector with good x</returns>
        public static Vector3 SubX(this Vector3 caller, float val)
        {
            return caller + Vector3.left * val;
        }

        /// <summary>
        /// sustrac a value to the vector's y
        /// </summary>
        /// <param name="caller">vector</param>
        /// <param name="val">the value to substrack</param>
        /// <returns>the vector with good y</returns>
        public static Vector3 SubY(this Vector3 caller, float val)
        {
            return caller + Vector3.down * val;
        }

        /// <summary>
        /// sustrac a value to the vector's z
        /// </summary>
        /// <param name="caller">vector</param>
        /// <param name="val">the value to substrack</param>
        /// <returns>the vector with good z</returns>
        public static Vector3 SubZ(this Vector3 caller, float val)
        {
            return caller + Vector3.back * val;
        }

        /// <summary>
        /// addition a value to the vector's x
        /// </summary>
        /// <param name="caller">vector</param>
        /// <param name="val">the value to addition</param>
        /// <returns>the vector with good x</returns>
        public static Vector3 SumX(this Vector3 caller, float val)
        {
            return caller + Vector3.right * val;
        }

        /// <summary>
        /// addition a value to the vector's y
        /// </summary>
        /// <param name="caller">vector</param>
        /// <param name="val">the value to addition</param>
        /// <returns>the vector with good y</returns>
        public static Vector3 SumY(this Vector3 caller, float val)
        {
            return caller + Vector3.up * val;
        }

        /// <summary>
        /// addition a value to the vector's z
        /// </summary>
        /// <param name="caller">vector</param>
        /// <param name="val">the value to addition</param>
        /// <returns>the vector with good z</returns>
        public static Vector3 SumZ(this Vector3 caller, float val)
        {
            return caller + Vector3.forward * val;
        }

    }

    public static class ArrayExtension
    {
        /// <summary>
        /// Add an element in a array
        /// </summary>
        /// <typeparam name="T">the type of array</typeparam>
        /// <param name="caller">the array called this function</param>
        /// <param name="element">element to add</param>
        /// <returns>new array</returns>
        public static T[] Add<T>(this T[] caller, T element)
        {
            T[] newTab = new T[caller.Length + 1];
            caller.CopyTo(newTab, 0);
            newTab[caller.Length] = element;
            return newTab;
        }

        /// <summary>
        /// Remove an element in a array
        /// </summary>
        /// <typeparam name="T">the type of array</typeparam>
        /// <param name="caller">the array called this function</param>
        /// <param name="element">element to add</param>
        /// <returns>new array</returns>
        public static T[] Remove<T>(this T[] caller, int index)
        {
            T[] newTab = new T[caller.Length - 1];
            int i2 = 0;

            for (int i = 0; i < caller.Length; i++)
            {
                if (i != index)
                {
                    newTab[i2] = caller[i];
                    i2++;
                }
            }
            return newTab;
        }

        /// <summary>
        /// convert an array to a List
        /// </summary>
        /// <typeparam name="T">the type of array</typeparam>
        /// <param name="caller">the array called this function</param>
        /// <returns>the list</returns>
        public static List<T> ToList<T>(this T[] caller)
        {
            List<T> newList = new List<T>();

            foreach (T item in caller)
                newList.Add(item);

            return newList;
        }
    }

    public static class ListExtension
    {
        /// <summary>
        /// convert a list to an array
        /// </summary>
        /// <typeparam name="T">the type of the list</typeparam>
        /// <param name="caller">the list called this function</param>
        /// <returns>the array</returns>
        public static T[] ToArray<T>(this List<T> caller)
        {
            T[] array = new T[caller.Count];

            for (int i = 0, l = caller.Count; i < l; i++)
                array[i] = caller[i];

            return array;
        }
    }

    public static class TransformExtension
    {
        /// <summary>
        /// get all childs of an object
        /// </summary>
        /// <param name="caller">the transform who call this function</param>
        /// <returns>transform array represent childs of the caller</returns>
        public static Transform[] GetChilds(this Transform caller)
        {
            Transform[] childs = new Transform[0];

            int lenght = caller.childCount;
            for (int i = 0; i < lenght; i++)
            {
                if (caller.GetChild(i))
                    childs = childs.Add(caller.GetChild(i));
            }
            return childs;
        }
    }

    public static class ComponentExtension
    {
        /// <summary>
        /// get all childs of an object
        /// </summary>
        /// <param name="caller">the component who call this function</param>
        /// <returns> childs of the caller</returns>
        public static GameObject[] GetChildsToGameObject(this Component caller)
        {
            Transform[] childs = caller.transform.GetChilds();
            GameObject[] goChilds = new GameObject[childs.Length];

            for (int i = 0, l = childs.Length; i < l; i++)
                goChilds[i] = childs[i].gameObject;

            return goChilds;
        }
    }

}
