﻿using System;

namespace TwistedLogik.Ultraviolet.Graphics.Graphics2D.Text
{
    /// <summary>
    /// Represents the output from lexing formatted text.
    /// </summary>
    public sealed class TextLexerResult : TextResult<TextLexerToken>
    {
        /// <inheritdoc/>
        public override void Clear()
        {
            Source = null;
            base.Clear();
        }
        
        /// <summary>
        /// Gets or sets the lexer's source string.
        /// </summary>
        internal StringSource? Source
        {
            get;
            set;
        }
    }
}
