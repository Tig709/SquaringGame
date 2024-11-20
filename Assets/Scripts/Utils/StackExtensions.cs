using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class StackExtensions
    {
        private static readonly Random random = new Random();

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
    }
}