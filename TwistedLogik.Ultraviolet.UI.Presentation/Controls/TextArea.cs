﻿using System;
using System.ComponentModel;
using System.Text;
using TwistedLogik.Nucleus;
using TwistedLogik.Ultraviolet.Input;
using TwistedLogik.Ultraviolet.UI.Presentation.Input;

namespace TwistedLogik.Ultraviolet.UI.Presentation.Controls
{
    /// <summary>
    /// Represents an element which allows the user to edit multiple lines of text.
    /// </summary>
    [UvmlKnownType(null, "TwistedLogik.Ultraviolet.UI.Presentation.Controls.Templates.TextArea.xml")]
    [DefaultProperty("Text")]
    public class TextArea : Control
    {
        /// <summary>
        /// Initializes the <see cref="TextArea"/> type.
        /// </summary>
        static TextArea()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextArea"/> class.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <param name="name">The element's identifying name within its namescope.</param>
        public TextArea(UltravioletContext uv, String name)
            : base(uv, name)
        {

        }

        /// <summary>
        /// Gets or sets a value specifying how the text area's text wraps when it reaches the edge of its container.
        /// </summary>
        public TextWrapping TextWrapping
        {
            get { return GetValue<TextWrapping>(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value); }
        }

        /// <summary>
        /// Gets or sets a <see cref="ScrollBarVisibility"/> value which specifies whether the text area's horizontal scroll bar is visible.
        /// </summary>
        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get { return GetValue<ScrollBarVisibility>(HorizontalScrollBarVisibilityProperty); }
            set { SetValue(HorizontalScrollBarVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets or sets a <see cref="ScrollBarVisibility"/> value which specifies whether the text area's vertical scroll bar is visible.
        /// </summary>
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return GetValue<ScrollBarVisibility>(VerticalScrollBarVisibilityProperty); }
            set { SetValue(VerticalScrollBarVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text area will accept the return key as a normal character.
        /// </summary>
        public Boolean AcceptsReturn
        {
            get { return GetValue<Boolean>(AcceptsReturnProperty); }
            set { SetValue(AcceptsReturnProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text area will accept the tab key as a normal character. If false,
        /// tab will instead be used for tab navigation.
        /// </summary>
        public Boolean AcceptsTab
        {
            get { return GetValue<Boolean>(AcceptsTabProperty); }
            set { SetValue(AcceptsTabProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="TextWrapping"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(TextArea),
            new PropertyMetadata<TextWrapping>(TextWrapping.NoWrap, PropertyMetadataOptions.AffectsMeasure, HandleTextWrappingChanged));

        /// <summary>
        /// Identifies the <see cref="HorizontalScrollBarVisibility"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty = DependencyProperty.Register("HorizontalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(TextArea),
            new PropertyMetadata<ScrollBarVisibility>(ScrollBarVisibility.Hidden, PropertyMetadataOptions.None, null, CoerceHorizontalScrollBarVisibility));

        /// <summary>
        /// Identifies the <see cref="VerticalScrollBarVisibility"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty = DependencyProperty.Register("VerticalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(TextArea),
            new PropertyMetadata<ScrollBarVisibility>(ScrollBarVisibility.Hidden, PropertyMetadataOptions.None));

        /// <summary>
        /// Identifies the <see cref="AcceptsReturn"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AcceptsReturnProperty = KeyboardNavigation.AcceptsReturnProperty.AddOwner(typeof(TextArea));

        /// <summary>
        /// Identifies the <see cref="AcceptsTab"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AcceptsTabProperty = DependencyProperty.Register("AcceptsTab", typeof(Boolean), typeof(TextArea),
            new PropertyMetadata<Boolean>(CommonBoxedValues.Boolean.False));

        /// <inheritdoc/>
        protected override void OnViewChanged(PresentationFoundationView oldView, PresentationFoundationView newView)
        {
            if (PART_ContentPresenter != null)
                PART_ContentPresenter.HandleTextBufferUpdate(textBuffer);

            base.OnViewChanged(oldView, newView);
        }

        /// <inheritdoc/>
        protected override void OnMouseDown(MouseDevice device, MouseButton button, ref RoutedEventData data)
        {
            if (button == MouseButton.Left)
            {
                Focus();
                CaptureMouse();
            }
            data.Handled = true;
            base.OnMouseDown(device, button, ref data);
        }

        /// <inheritdoc/>
        protected override void OnMouseUp(MouseDevice device, MouseButton button, ref RoutedEventData data)
        {
            if (button == MouseButton.Left)
            {
                ReleaseMouseCapture();
            }
            data.Handled = true;
            base.OnMouseUp(device, button, ref data);
        }

        /// <inheritdoc/>
        protected override void OnKeyDown(KeyboardDevice device, Key key, ModifierKeys modifiers, ref RoutedEventData data)
        {
            switch (key)
            {
                case Key.Return:
                    if (AcceptsReturn)
                    {
                        ProcessReturn();
                        data.Handled = true;
                    }
                    break;

                case Key.Tab:
                    if (AcceptsTab)
                    {
                        ProcessTab();
                        data.Handled = true;
                    }
                    break;

                case Key.Backspace:
                    ProcessBackspace();
                    data.Handled = true;
                    break;

                case Key.Delete:
                    ProcessDelete();
                    data.Handled = true;
                    break;
            }
            base.OnKeyDown(device, key, modifiers, ref data);
        }

        /// <inheritdoc/>
        protected override void OnTextInput(KeyboardDevice device, ref RoutedEventData data)
        {
            device.GetTextInput(tempBuffer);

            // TODO
            textBuffer.Append(tempBuffer);

            if (PART_ContentPresenter != null)
                PART_ContentPresenter.HandleTextBufferUpdate(textBuffer);

            base.OnTextInput(device, ref data);
        }

        /// <summary>
        /// Occurs when the value of the <see cref="TextWrapping"/> dependency property changes.
        /// </summary>
        private static void HandleTextWrappingChanged(DependencyObject dobj, TextWrapping oldValue, TextWrapping newValue)
        {
            var textArea = (TextArea)dobj;
            textArea.CoerceValue(HorizontalScrollBarVisibilityProperty);

            if (textArea.PART_ContentPresenter != null)
                textArea.PART_ContentPresenter.InvalidateMeasure();
        }

        /// <summary>
        /// Coerces the value of the <see cref="HorizontalScrollBarVisibility"/> property to force the scroll bar
        /// to a disabled state when wrapping is enabled.
        /// </summary>
        private static ScrollBarVisibility CoerceHorizontalScrollBarVisibility(DependencyObject dobj, ScrollBarVisibility value)
        {
            var textArea = (TextArea)dobj;
            if (textArea.TextWrapping == TextWrapping.Wrap)
            {
                return ScrollBarVisibility.Disabled;
            }
            return value;
        }

        /// <summary>
        /// Inserts or appends a return character at the caret.
        /// </summary>
        private void ProcessReturn()
        {
            textBuffer.Append('\n');

            if (PART_ContentPresenter != null)
                PART_ContentPresenter.HandleTextBufferUpdate(textBuffer);
        }

        /// <summary>
        /// Inserts or appends a return character at the caret.
        /// </summary>
        private void ProcessTab()
        {
            textBuffer.Append('\t');

            if (PART_ContentPresenter != null)
                PART_ContentPresenter.HandleTextBufferUpdate(textBuffer);
        }

        /// <summary>
        /// Removes the character before the caret.
        /// </summary>
        private void ProcessBackspace()
        {
            if (textBuffer.Length > 0)
            {
                textBuffer.Remove(textBuffer.Length - 1, 1);

                if (PART_ContentPresenter != null)
                    PART_ContentPresenter.HandleTextBufferUpdate(textBuffer);
            }
        }

        /// <summary>
        /// Removes the character after the caret.
        /// </summary>
        private void ProcessDelete()
        {

        }        

        // Component references.
        private readonly TextAreaContentPresenter PART_ContentPresenter = null;

        // State values.
        private readonly StringBuilder tempBuffer = new StringBuilder();
        private readonly StringBuilder textBuffer = new StringBuilder();
    }
}
