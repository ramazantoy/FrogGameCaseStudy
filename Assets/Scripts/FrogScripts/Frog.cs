using System;
using Enums;
using HexViewScripts;
using UnityEngine;

namespace FrogScripts
{
    /// <summary>
    /// Represents a frog element on the hex grid, managing its properties and actions.
    /// </summary>
    public class Frog : HexViewElement
    {
        [SerializeField]
        private FrogData _properties;
        
        /// <summary>
        /// Gets or sets the direction of the frog.
        /// </summary>
        public Direction FrogDirection { get; set; }
    
        /// <summary>
        /// Executes the action associated with the frog.
        /// </summary>
        public override void OnAction()
        {
            // Action logic for the frog
        }

        public override void SetColor(ColorType colorType)
        {
            _properties.SetFrogColor(colorType);
        }
    }
}
