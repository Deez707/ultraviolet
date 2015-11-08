﻿using System;
using TwistedLogik.Nucleus.Text;

namespace TwistedLogik.Ultraviolet.Graphics.Graphics2D.Text
{
    /// <summary>
    /// Represents the result of parsing formatted text.
    /// </summary>
    public sealed class TextParserResult : TextResult<TextParserToken>
    {
        /// <summary>
        /// Clears the result.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
            styles.Clear();
        }

        /// <summary>
        /// Adds a token to the result using the current style.
        /// </summary>
        /// <param name="text">The token's text.</param>
        internal void AddToken(StringSegment text)
        {
            var token = new TextParserToken(text, Styles.Count - 1);
            Add(token);
        }

        /// <summary>
        /// Adds a style to the result.
        /// </summary>
        /// <param name="style">The style to add to the result.</param>
        internal void AddStyle(TextStyle style)
        {
            styles.Add(style);
        }

        /// <summary>
        /// Adds a style to the result with the specified value for boldness.
        /// </summary>
        /// <param name="style">The base style to add to the result.</param>
        /// <param name="bold">The bold value to set on the style.</param>
        internal void AddStyleWithBold(ref TextStyle style, Boolean bold)
        {
            style.Bold = bold;
            styles.Add(style);
        }

        /// <summary>
        /// Adds a style to the result with the specified value for italics.
        /// </summary>
        /// <param name="style">The base style to add to the result.</param>
        /// <param name="italic">The italic value to set on the style.</param>
        internal void AddStyleWithItalic(ref TextStyle style, Boolean italic)
        {
            style.Italic = italic;
            styles.Add(style);
        }

        /// <summary>
        /// Adds a style to the result with the specified color.
        /// </summary>
        /// <param name="style">The base style to add to the result.</param>
        /// <param name="color">The color to set on the style.</param>
        internal void AddStyleWithColor(ref TextStyle style, Color? color)
        {
            style.Color = color;
            styles.Add(style);
        }

        /// <summary>
        /// Adds a style to the result with the specified font.
        /// </summary>
        /// <param name="style">The base style to add to the result.</param>
        /// <param name="font">The font to set on the style.</param>
        internal void AddStyleWithFont(ref TextStyle style, StringSegment? font)
        {
            style.Font = font;
            styles.Add(style);
        }

        /// <summary>
        /// Adds a style to the result with the specified icon.
        /// </summary>
        /// <param name="style">The base style to add to the result.</param>
        /// <param name="icon">The icon to set on the style.</param>
        internal void AddStyleWithIcon(ref TextStyle style, StringSegment? icon)
        {
            style.Icon = icon;
            styles.Add(style);
        }

        /// <summary>
        /// Adds a style to the result with the specified glyph shader.
        /// </summary>
        /// <param name="style">The base style to add to the result.</param>
        /// <param name="glyphShader">The glyph shader to set on the style.</param>
        internal void AddStyleWithGlyphShader(ref TextStyle style, StringSegment? glyphShader)
        {
            style.GlyphShader = glyphShader;
            styles.Add(style);
        }

        /// <summary>
        /// Adds a style to the result with the specified style preset.
        /// </summary>
        /// <param name="style">The base style to add to the result.</param>
        /// <param name="preset">The style preset to set on the style.</param>
        internal void AddStyleWithStyle(ref TextStyle style, StringSegment? preset)
        {
            style.Style = preset;
            styles.Add(style);
        }

        /// <summary>
        /// Gets the list of styles generated by the parser.
        /// </summary>
        public TextParserResultStyles Styles
        {
            get { return styles; }
        }

        // The styles used by this result's tokens.
        private readonly TextParserResultStyles styles = new TextParserResultStyles();
    }
}
