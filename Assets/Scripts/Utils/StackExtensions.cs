using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class StackExtensions
    {
        private static readonly System.Random random = new System.Random();

        /// <summary>
        /// Shuffles a stack
        /// </summary>
        /// <typeparam name="T">Type of elements in stack</typeparam>
        /// <param name="stack">The stack to shuffle</param>
        public static void Shuffle<T>(this Stack<T> stack)
        {
            var values = stack.ToArray();
            stack.Clear();

            foreach (var value in values.OrderBy(x => random.Next()))
                stack.Push(value);
        }

        public static List<T> ToList<T>(this Stack<T> stack)
        {
            // Create a new list to store the stack elements
            List<T> list = new List<T>(stack.Count);

            // Iterate over the stack, starting from the top
            foreach (var item in stack)
            {
                list.Add(item);
            }

            return list;
        }

        public static List<GameObject> ToGameObjectList(this Stack<Card> stack)
        {
            // Create a new list to store the GameObjects
            List<GameObject> list = new List<GameObject>(stack.Count);

            // Iterate over the stack, starting from the top
            foreach (var card in stack)
            {
                // Add the GameObject from the Card to the list
                list.Add(card.gameObject);  // Assuming Card has a gameObject field or property
            }

            return list;
        }
    }
}