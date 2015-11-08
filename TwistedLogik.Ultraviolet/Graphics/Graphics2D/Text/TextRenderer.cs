﻿using System;
using System.Text;
using TwistedLogik.Nucleus;
using TwistedLogik.Nucleus.Text;

namespace TwistedLogik.Ultraviolet.Graphics.Graphics2D.Text
{
    /// <summary>
    /// Contains methods for rendering formatted text.
    /// </summary>
    public class TextRenderer
    {
        /// <summary>
        /// Registers a style with the specified name.
        /// </summary>
        /// <param name="name">The name of the style to register.</param>
        /// <param name="style">The style to register.</param>
        public void RegisterStyle(String name, TextStyle style)
        {
            layoutEngine.RegisterStyle(name, style);
        }

        /// <summary>
        /// Unregisters the style with the specified name.
        /// </summary>
        /// <param name="name">The name of the style to unregister.</param>
        /// <returns><c>true</c> if the style was unregistered; otherwise, <c>false</c>.</returns>
        public Boolean UnregisterStyle(String name)
        {
            return layoutEngine.UnregisterStyle(name);
        }

        /// <summary>
        /// Registers the font with the specified name.
        /// </summary>
        /// <param name="name">The name of the font to register.</param>
        /// <param name="font">The font to register.</param>
        public void RegisterFont(String name, SpriteFont font)
        {
            layoutEngine.RegisterFont(name, font);
        }

        /// <summary>
        /// Unregisters the font with the specified name.
        /// </summary>
        /// <param name="name">The name of the font to unregister.</param>
        /// <returns><c>true</c> if the font was unregistered; otherwise, <c>false</c>.</returns>
        public Boolean UnregisterFont(String name)
        {
            return layoutEngine.UnregisterFont(name);
        }

        /// <summary>
        /// Registers the icon with the specified name.
        /// </summary>
        /// <param name="name">The name of the icon to register.</param>
        /// <param name="icon">The icon to register.</param>
        /// <param name="height">The width to which to scale the icon, or null to preserve the sprite's original width.</param>
        /// <param name="width">The height to which to scale the icon, or null to preserve the sprite's original height.</param>
        public void RegisterIcon(String name, SpriteAnimation icon, Int32? width = null, Int32? height = null)
        {
            layoutEngine.RegisterIcon(name, icon, width, height);
        }

        /// <summary>
        /// Unregisters the icon with the specified name.
        /// </summary>
        /// <param name="name">The name of the icon to unregister.</param>
        /// <returns><c>true</c> if the icon was unregistered; otherwise, <c>false</c>.</returns>
        public Boolean UnregisterIcon(String name)
        {
            return layoutEngine.UnregisterIcon(name);
        }

        /// <summary>
        /// Registers the glyph shader with the specified name.
        /// </summary>
        /// <param name="name">The name of the glyph shader to register.</param>
        /// <param name="shader">The glyph shader to register.</param>
        public void RegisterGlyphShader(String name, GlyphShader shader)
        {
            layoutEngine.RegisterGlyphShader(name, shader);
        }

        /// <summary>
        /// Unregisters the glyph shader with the specified name.
        /// </summary>
        /// <param name="name">The name of the glyph shader to unregister.</param>
        /// <returns><c>true</c> if the glyph shader was unregistered; otherwise, <c>false</c>.</returns>
        public Boolean UnregisterGlyphShader(String name)
        {
            return layoutEngine.UnregisterGlyphShader(name);
        }

        /// <summary>
        /// Lexes the specified string.
        /// </summary>
        /// <param name="input">The string to lex.</param>
        /// <param name="output">The lexed token stream.</param>
        public void Lex(String input, TextLexerResult output)
        {
            Contract.Require(input, "input");
            Contract.Require(output, "output");

            lexer.Lex(input, output);
        }

        /// <summary>
        /// Parses the specified string.
        /// </summary>
        /// <param name="input">The token stream to parse.</param>
        /// <param name="output">The parsed token stream.</param>
        public void Parse(String input, TextParserResult output)
        {
            Contract.Require(input, "input");
            Contract.Require(output, "output");

            lexer.Lex(input, lexerResult);
            parser.Parse(lexerResult, output);
        }

        /// <summary>
        /// Parses the specified token stream.
        /// </summary>
        /// <param name="input">The token stream to parse.</param>
        /// <param name="output">The parsed token stream.</param>
        public void Parse(TextLexerResult input, TextParserResult output)
        {
            Contract.Require(input, "input");
            Contract.Require(output, "output");

            parser.Parse(lexerResult, output);
        }

        /// <summary>
        /// Calculates a layout for the specified text.
        /// </summary>
        /// <param name="input">The string of text to lay out.</param>
        /// <param name="output">The formatted text with layout information.</param>
        /// <param name="settings">The layout settings.</param>
        public void CalculateLayout(String input, TextLayoutResult output, TextLayoutSettings settings)
        {
            Contract.Require(input, "input");
            Contract.Require(output, "output");

            lexer.Lex(input, lexerResult);
            parser.Parse(lexerResult, parserResult);
            layoutEngine.CalculateLayout(parserResult, output, settings);
        }

        /// <summary>
        /// Calculates a layout for the specified text.
        /// </summary>
        /// <param name="input">The lexed text to lay out.</param>
        /// <param name="output">The formatted text with layout information.</param>
        /// <param name="settings">The layout settings.</param>
        public void CalculateLayout(TextLexerResult input, TextLayoutResult output, TextLayoutSettings settings)
        {
            Contract.Require(input, "input");
            Contract.Require(output, "output");

            parser.Parse(input, parserResult);
            layoutEngine.CalculateLayout(parserResult, output, settings);
        }

        /// <summary>
        /// Calculates a layout for the specified text.
        /// </summary>
        /// <param name="input">The parsed text to lay out.</param>
        /// <param name="output">The formatted text with layout information.</param>
        /// <param name="settings">The layout settings.</param>
        public void CalculateLayout(TextParserResult input, TextLayoutResult output, TextLayoutSettings settings)
        {
            Contract.Require(input, "input");
            Contract.Require(output, "output");

            layoutEngine.CalculateLayout(input, output, settings);
        }

        /// <summary>
        /// Draws a string of formatted text.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch with which to draw the text.</param>
        /// <param name="input">The string to draw.</param>
        /// <param name="position">The position in screen coordinates at which to draw.</param>
        /// <param name="color">The color with which to draw the text.</param>
        /// <param name="settings">The layout settings.</param>
        /// <returns>A <see cref="RectangleF"/> representing area in which the text was drawn.</returns>
        public RectangleF Draw(SpriteBatch spriteBatch, String input, Vector2 position, Color color, TextLayoutSettings settings)
        {
            Contract.Require(spriteBatch, "spriteBatch");
            Contract.Require(input, "input");

            lexer.Lex(input, lexerResult);

            var parserOptions = GetParserOptions(ref settings);
            parser.Parse(lexerResult, parserResult, parserOptions);

            layoutEngine.CalculateLayout(parserResult, layoutResult, settings);

            return Draw(spriteBatch, layoutResult, position, color, 0, Int32.MaxValue);
        }

        /// <summary>
        /// Draws a string of formatted text.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch with which to draw the text.</param>
        /// <param name="input">The string to draw.</param>
        /// <param name="position">The position in screen coordinates at which to draw.</param>
        /// <param name="color">The color with which to draw the text.</param>
        /// <param name="start">The index of the first glyph to render.</param>
        /// <param name="count">The number of glyphs to render.</param>
        /// <param name="settings">The layout settings.</param>
        /// <returns>A <see cref="RectangleF"/> representing area in which the text was drawn.</returns>
        public RectangleF Draw(SpriteBatch spriteBatch, String input, Vector2 position, Color color, Int32 start, Int32 count, TextLayoutSettings settings)
        {
            Contract.Require(spriteBatch, "spriteBatch");
            Contract.Require(input, "input");

            lexer.Lex(input, lexerResult);

            var parserOptions = GetParserOptions(ref settings);
            parser.Parse(lexerResult, parserResult, parserOptions);

            layoutEngine.CalculateLayout(parserResult, layoutResult, settings);

            return Draw(spriteBatch, layoutResult, position, color, start, count);
        }

        /// <summary>
        /// Draws a string of formatted text.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch with which to draw the text.</param>
        /// <param name="input">The string to draw.</param>
        /// <param name="position">The position in screen coordinates at which to draw.</param>
        /// <param name="color">The color with which to draw the text.</param>
        /// <param name="settings">The layout settings.</param>
        /// <returns>A <see cref="RectangleF"/> representing area in which the text was drawn.</returns>
        public RectangleF Draw(SpriteBatch spriteBatch, StringBuilder input, Vector2 position, Color color, TextLayoutSettings settings)
        {
            Contract.Require(spriteBatch, "spriteBatch");
            Contract.Require(input, "input");

            lexer.Lex(input, lexerResult);

            var parserOptions = GetParserOptions(ref settings);
            parser.Parse(lexerResult, parserResult, parserOptions);

            layoutEngine.CalculateLayout(parserResult, layoutResult, settings);

            return Draw(spriteBatch, layoutResult, position, color, 0, Int32.MaxValue);
        }

        /// <summary>
        /// Draws a string of formatted text.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch with which to draw the text.</param>
        /// <param name="input">The string to draw.</param>
        /// <param name="position">The position in screen coordinates at which to draw.</param>
        /// <param name="color">The color with which to draw the text.</param>
        /// <param name="start">The index of the first glyph to render.</param>
        /// <param name="count">The number of glyphs to render.</param>
        /// <param name="settings">The layout settings.</param>
        /// <returns>A <see cref="RectangleF"/> representing area in which the text was drawn.</returns>
        public RectangleF Draw(SpriteBatch spriteBatch, StringBuilder input, Vector2 position, Color color, Int32 start, Int32 count, TextLayoutSettings settings)
        {
            Contract.Require(spriteBatch, "spriteBatch");
            Contract.Require(input, "input");

            lexer.Lex(input, lexerResult);

            var parserOptions = GetParserOptions(ref settings);
            parser.Parse(lexerResult, parserResult, parserOptions);

            layoutEngine.CalculateLayout(parserResult, layoutResult, settings);

            return Draw(spriteBatch, layoutResult, position, color, start, count);
        }

        /// <summary>
        /// Draws a string of formatted text.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch with which to draw the text.</param>
        /// <param name="input">The token stream to draw.</param>
        /// <param name="position">The position in screen coordinates at which to draw.</param>
        /// <param name="color">The color with which to draw the text.</param>
        /// <param name="settings">The layout settings.</param>
        /// <returns>A <see cref="RectangleF"/> representing area in which the text was drawn.</returns>
        public RectangleF Draw(SpriteBatch spriteBatch, TextLexerResult input, Vector2 position, Color color, TextLayoutSettings settings)
        {
            Contract.Require(spriteBatch, "spriteBatch");
            Contract.Require(input, "input");

            var parserOptions = GetParserOptions(ref settings);
            parser.Parse(input, parserResult, parserOptions);

            layoutEngine.CalculateLayout(parserResult, layoutResult, settings);

            return Draw(spriteBatch, layoutResult, position, color, 0, Int32.MaxValue);
        }

        /// <summary>
        /// Draws a string of formatted text.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch with which to draw the text.</param>
        /// <param name="input">The token stream to draw.</param>
        /// <param name="position">The position in screen coordinates at which to draw.</param>
        /// <param name="color">The color with which to draw the text.</param>
        /// <param name="start">The index of the first glyph to render.</param>
        /// <param name="count">The number of glyphs to render.</param>
        /// <param name="settings">The layout settings.</param>
        /// <returns>A <see cref="RectangleF"/> representing area in which the text was drawn.</returns>
        public RectangleF Draw(SpriteBatch spriteBatch, TextLexerResult input, Vector2 position, Color color, Int32 start, Int32 count, TextLayoutSettings settings)
        {
            Contract.Require(spriteBatch, "spriteBatch");
            Contract.Require(input, "input");

            var parserOptions = GetParserOptions(ref settings);
            parser.Parse(input, parserResult, parserOptions);

            layoutEngine.CalculateLayout(parserResult, layoutResult, settings);

            return Draw(spriteBatch, layoutResult, position, color, start, count);
        }

        /// <summary>
        /// Draws a string of formatted text.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch with which to draw the text.</param>
        /// <param name="input">The token stream to draw.</param>
        /// <param name="position">The position in screen coordinates at which to draw.</param>
        /// <param name="color">The color with which to draw the text.</param>
        /// <param name="settings">The layout settings.</param>
        /// <returns>A <see cref="RectangleF"/> representing area in which the text was drawn.</returns>
        public RectangleF Draw(SpriteBatch spriteBatch, TextParserResult input, Vector2 position, Color color, TextLayoutSettings settings)
        {
            Contract.Require(spriteBatch, "spriteBatch");
            Contract.Require(input, "input");

            layoutEngine.CalculateLayout(input, layoutResult, settings);

            return Draw(spriteBatch, layoutResult, position, color, 0, Int32.MaxValue);
        }

        /// <summary>
        /// Draws a string of formatted text.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch with which to draw the text.</param>
        /// <param name="input">The token stream to draw.</param>
        /// <param name="position">The position in screen coordinates at which to draw.</param>
        /// <param name="color">The color with which to draw the text.</param>
        /// <param name="start">The index of the first glyph to render.</param>
        /// <param name="count">The number of glyphs to render.</param>
        /// <param name="settings">The layout settings.</param>
        /// <returns>A <see cref="RectangleF"/> representing area in which the text was drawn.</returns>
        public RectangleF Draw(SpriteBatch spriteBatch, TextParserResult input, Vector2 position, Color color, Int32 start, Int32 count, TextLayoutSettings settings)
        {
            Contract.Require(spriteBatch, "spriteBatch");
            Contract.Require(input, "input");

            layoutEngine.CalculateLayout(input, layoutResult, settings);

            return Draw(spriteBatch, layoutResult, position, color, start, count);
        }

        /// <summary>
        /// Draws a string of formatted text.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch with which to draw the text.</param>
        /// <param name="input">The token stream to draw.</param>
        /// <param name="position">The position in screen coordinates at which to draw.</param>
        /// <param name="color">The color with which to draw the text.</param>
        /// <returns>A <see cref="RectangleF"/> representing area in which the text was drawn.</returns>
        public RectangleF Draw(SpriteBatch spriteBatch, TextLayoutResult input, Vector2 position, Color color)
        {
            return Draw(spriteBatch, input, position, color, 0, Int32.MaxValue);
        }

        /// <summary>
        /// Draws a string of formatted text.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch with which to draw the text.</param>
        /// <param name="input">The token stream to draw.</param>
        /// <param name="position">The position in screen coordinates at which to draw.</param>
        /// <param name="color">The color with which to draw the text.</param>
        /// <param name="start">The index of the first glyph to render.</param>
        /// <param name="count">The number of glyphs to render.</param>
        /// <returns>A <see cref="RectangleF"/> representing area in which the text was drawn.</returns>
        public RectangleF Draw(SpriteBatch spriteBatch, TextLayoutResult input, Vector2 position, Color color, Int32 start, Int32 count)
        {
            Contract.Require(spriteBatch, "spriteBatch");
            Contract.Require(input, "input");

            if (count <= 0)
                return RectangleF.Empty;

            var alpha = color.A / (float)Byte.MaxValue;

            var scissor         = spriteBatch.Ultraviolet.GetGraphics().GetScissorRectangle();
            var scissorRect     = scissor.GetValueOrDefault();
            var scissorClipping = scissor.HasValue;

            if (scissorClipping && !IsScissorClippingOptimizationPossibleForTransform(spriteBatch.CurrentTransformMatrix))
                scissorClipping = false;

            var glyphClipping = (start > 0 || count < Int32.MaxValue);
            var glyphsSeen = 0;
            var glyphsDrawn = 0;

            foreach (var token in input)
            {
                var skipToken = false;

                var tokenIndexInSource = glyphsSeen;
                var tokenStart = 0;
                var tokenLength = token.Text.Length;
                var tokenBounds = token.Bounds;

                if (glyphClipping)
                {
                    if (glyphsDrawn >= count)
                        break;

                    if (token.Icon == null)
                    {
                        if (glyphsSeen + tokenLength > start)
                        {
                            if (glyphsSeen < start && glyphsSeen + tokenLength >= start)
                            {
                                tokenStart = start - glyphsSeen;
                                tokenLength = tokenLength - tokenStart;
                            }

                            if (glyphsDrawn + tokenLength > count)
                            {
                                tokenLength = count - glyphsDrawn;
                            }
                        }
                        else
                        {
                            skipToken = true;
                        }
                    }
                }

                glyphsSeen += token.Text.Length;

                if (skipToken)
                    continue;

                if (scissorClipping)
                {
                    var translation = spriteBatch.CurrentTransformMatrix.Translation;
                    if (translation.Y + position.Y + tokenBounds.Bottom < scissorRect.Top ||
                        translation.Y + position.Y + tokenBounds.Top > scissorRect.Bottom ||
                        translation.X + position.X + tokenBounds.Right < scissorRect.Left ||
                        translation.X + position.X + tokenBounds.Left > scissorRect.Right)
                    {
                        continue;
                    }
                }

                if (token.Icon != null)
                {
                    var iconInfo  = token.Icon.Value;
                    var iconAnimation = iconInfo.Icon;
                    var iconOrigin = iconAnimation.Controller.GetFrame().Origin;
                    var iconX = position.X + tokenBounds.X + iconOrigin.X;
                    var iconY = position.Y + tokenBounds.Y + iconOrigin.Y;
                    var iconColor = color;

                    var tokenGlyphShaderContext = token.GlyphShader == null ? GlyphShaderContext.Invalid :
                        new GlyphShaderContext(token.GlyphShader, tokenIndexInSource, layoutResult.TotalLength);

                    if (token.GlyphShader != null)
                    {
                        var iconGlyph = (char)0;
                        token.GlyphShader.Execute(ref tokenGlyphShaderContext, ref iconGlyph, ref iconX, ref iconY, ref iconColor, 0);
                    }

                    var tokenPosition = new Vector2(iconX, iconY);
                    spriteBatch.DrawSprite(iconAnimation.Controller, tokenPosition, iconInfo.Width, iconInfo.Height, iconColor * alpha, 0f);
                }
                else
                {
                    var tokenX = position.X + tokenBounds.X + (tokenStart == 0 ? 0 : token.FontFace.MeasureString(new StringSegment(token.Text, 0, tokenStart)).Width);
                    var tokenY = position.Y + tokenBounds.Y;
                    var tokenPosition = new Vector2(tokenX, tokenY);

                    var tokenGlyphShaderContext = token.GlyphShader == null ? GlyphShaderContext.Invalid :
                        new GlyphShaderContext(token.GlyphShader, tokenIndexInSource, layoutResult.TotalLength);

                    if (tokenLength != token.Text.Length)
                    {
                        var tokenText = new StringSegment(token.Text, tokenStart, tokenLength);
                        spriteBatch.DrawString(tokenGlyphShaderContext, token.FontFace, tokenText, tokenPosition, (token.Color * alpha) ?? color);
                    }
                    else
                    {
                        spriteBatch.DrawString(tokenGlyphShaderContext, token.FontFace, token.Text, tokenPosition, (token.Color * alpha) ?? color);
                    }

                    glyphsDrawn += tokenLength;
                }
            }

            return new RectangleF(position.X + input.Bounds.X, position.Y + input.Bounds.Y, input.Bounds.Width, input.Bounds.Height);
        }

        /// <summary>
        /// Gets a value indicating whether the text renderer can optimize text rendering under the specified transform.
        /// The optimization in question stops rendering if we leave the scissor rectangle; this is not currently possible
        /// if the sprite batch is being rotated or scaled.
        /// </summary>
        /// <param name="matrix">The transform matrix to evaluate.</param>
        /// <returns><c>true</c> if the specified matrix allows for the optimization to take place; otherwise, <c>false</c>.</returns>
        private static Boolean IsScissorClippingOptimizationPossibleForTransform(Matrix matrix)
        {
            return
                matrix.M11 == 1 && matrix.M12 == 0 && matrix.M13 == 0 &&
                matrix.M21 == 0 && matrix.M22 == 1 && matrix.M23 == 0 &&
                matrix.M31 == 0 && matrix.M32 == 0 && matrix.M33 == 1 &&
                matrix.M41 == 0 && matrix.M42 == 0 && matrix.M43 == 0 && matrix.M44 == 1;
        }

        /// <summary>
        /// Gets a set of <see cref="TextParserOptions"/> values that correspond to the specified layout settings.
        /// </summary>
        private static TextParserOptions GetParserOptions(ref TextLayoutSettings settings)
        {
            var options = TextParserOptions.None;

            if ((settings.Flags & TextFlags.IgnoreCommandCodes) == TextFlags.IgnoreCommandCodes)
                options |= TextParserOptions.IgnoreCommandCodes;

            return options;
        }

        // The lexer and parser used to process input text.
        private readonly TextLexer lexer = new TextLexer();
        private readonly TextLexerResult lexerResult = new TextLexerResult();
        private readonly TextParser parser = new TextParser();
        private readonly TextParserResult parserResult = new TextParserResult();
        private readonly TextLayoutEngine layoutEngine = new TextLayoutEngine();
        private readonly TextLayoutResult layoutResult = new TextLayoutResult();
    }
}
