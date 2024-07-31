



using System.Collections.Generic;

using UnityEngine;

using DVAH2ten.Core;

namespace DVAH2ten.Game.Common
{
    /// <summary>
    /// The class used for the color bomb + color bomb combo.
    /// </summary>
    public class TwoColorBombCombo : ColorBombCombo
    {
        /// <summary>
        /// Resolves this combo.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <param name="tiles">The tiles destroyed by the combo.</param>
        /// <param name="fxPool">The pool to use for the visual effects.</param>
        public override void Resolve(GameBoard board, List<GameObject> tiles, FxPool fxPool)
        {
            for (var i = tiles.Count - 1; i >= 0; i--)
            {
                var tile = tiles[i];
                if (tile != null && (tile.GetComponent<Candy>() != null || tile.GetComponent<ColorBomb>() != null))
                {
                    board.ExplodeTileNonRecursive(tile);
                }
            }

            //SoundManager.instance.PlaySound("ColorBomb");

            board.ApplyGravity();
        }
    }
}
