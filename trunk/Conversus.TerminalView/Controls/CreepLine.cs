using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Conversus.TerminalView.Controls
{
    [TemplatePart(Name = PART_CONTAINER, Type = typeof(FrameworkElement))]
	[TemplatePart(Name = PART_CONTENT, Type = typeof(FrameworkElement))]
	public class CreepLine : ContentControl
	{
		private const string PART_CONTAINER = "CONTAINER_PART";
		private const string PART_CONTENT = "CONTENT_PART";

		public const string NormalState = "Normal";
		public const string MarqueeState = "Marquee";

		FrameworkElement container;
		FrameworkElement content;
		string gotoStateName = null;
		DispatcherTimer delayTimer;

		public CreepLine()
		{
			this.DefaultStyleKey = typeof(CreepLine);

			delayTimer = new DispatcherTimer();
			delayTimer.Tick += delayTimer_Tick;
			delayTimer.Interval = TimeSpan.FromMilliseconds(1);
			delayTimer.Stop();
			OnInitialDelayChanged(TimeSpan.Zero, TimeSpan.Zero);

			IsEnabled = false;

			//SizeChanged += delegate { ClipToBounds(); };
			Loaded += delegate
			{
				//ClipToBounds();
				CalculateProperties(true, true);
			};
			IsEnabledChanged += delegate
			{
				if (IsEnabled) InvalidateState();
				else ImmediatllyGoToState(NormalState);
			};
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			FrameworkElement root = this.Content as FrameworkElement;
			if (root == null) return base.MeasureOverride(availableSize);
			root.Measure(availableSize);

			Size resultSize = availableSize;
			switch (HorizontalAlignment)
			{
				case System.Windows.HorizontalAlignment.Left:
				case System.Windows.HorizontalAlignment.Center:
				case System.Windows.HorizontalAlignment.Right:
					resultSize.Width = !double.IsInfinity(availableSize.Width) ? Math.Min(availableSize.Width, root.DesiredSize.Width) : root.DesiredSize.Width;
					break;
				case System.Windows.HorizontalAlignment.Stretch:
				default:
					resultSize.Width = !double.IsInfinity(availableSize.Width) ? availableSize.Width : root.DesiredSize.Width;
					break;
			}

			switch (VerticalAlignment)
			{
				case System.Windows.VerticalAlignment.Top:
				case System.Windows.VerticalAlignment.Center:
				case System.Windows.VerticalAlignment.Bottom:
					resultSize.Height = !double.IsInfinity(availableSize.Height) ? Math.Min(availableSize.Height, root.DesiredSize.Height) : root.DesiredSize.Height;
					break;
				case System.Windows.VerticalAlignment.Stretch:
				default:
					resultSize.Height = !double.IsInfinity(availableSize.Height) ? availableSize.Height : root.DesiredSize.Height;
					break;
			}

			return resultSize;
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			container = GetTemplatePart<FrameworkElement, Canvas>(PART_CONTAINER);
			content = GetTemplatePart<FrameworkElement, ContentControl>(PART_CONTENT);
			CalculateProperties(true, true);

			content.SizeChanged += delegate
			{
				this.InvalidateMeasure();
				CalculateProperties(false, true);
			};
			container.SizeChanged += delegate
			{
				this.InvalidateMeasure();
				CalculateProperties(true, false);
			};
		}

		private T GetTemplatePart<T, IfNotFoundType>(string templateName)
			where T : class
			where IfNotFoundType : T, new()
		{
			return (this.GetTemplateChild(templateName) as T) ?? new IfNotFoundType();
		}

        //private void ClipToBounds()
        //{
        //    Clip = new RectangleGeometry() { Rect = new Rect(0, 0, ActualWidth, ActualHeight) };
        //}

		private void CalculateProperties(bool containerChanged, bool contentChanged)
		{
			if (container == null || content == null) return;

			if (containerChanged)
			{
				StartContentAtRight = container.ActualWidth;
			}
			if (contentChanged)
			{
				EndContentAtLeft = -content.ActualWidth;
				this.MinHeight = content.ActualHeight;
			}
			if (containerChanged || contentChanged)
			{
				EndContentAtRight = container.ActualWidth - content.ActualWidth;
				CalculateDuration();
				//makesSenseToAnimate = EndContentAtRight < 0;
				InvalidateState();
			}
		}
		private void CalculateDuration()
		{
			if (container == null || content == null) return;
			Duration = new Duration(
				TimeSpan.FromSeconds(
					(container.ActualWidth * 2 + content.ActualWidth) / PixelsPerSecondSpeed));
		}

		private void delayTimer_Tick(object sender, EventArgs e)
		{
			if (!ImmediatllyGoToState(gotoStateName)) return;

			delayTimer.Stop();
			gotoStateName = null;
		}
		private void InvalidateState()
		{
			GoToState(MarqueeState);
		}
		private void GoToState(string stateName)
		{
			if (!IsEnabled)
			{
				ImmediatllyGoToState(NormalState);
				return;
			}
			if (stateName == NormalState)
				ImmediatllyGoToState(stateName);
			else
				DellayGoToState(stateName);
		}
		private void DellayGoToState(string stateName)
		{
			gotoStateName = stateName;
			delayTimer.Start();
		}
		private bool ImmediatllyGoToState(string stateName)
		{
			try
			{
				CalculateDuration();
				if (string.IsNullOrWhiteSpace(stateName)) stateName = NormalState;
				VisualStateManager.GoToState(this, stateName, true);
				delayTimer.Stop();
				gotoStateName = null;
				return true;
			}
			catch
			{
				return false;
			}
		}


		public static readonly DependencyProperty StartContentAtRightProperty = DependencyProperty.Register("StartContentAtRight", typeof(double), typeof(CreepLine), new PropertyMetadata(0.0));
		public double StartContentAtRight
		{
			get { return (double)GetValue(StartContentAtRightProperty); }
			set { SetValue(StartContentAtRightProperty, value); }
		}
		public static readonly DependencyProperty EndContentAtLeftProperty = DependencyProperty.Register("EndContentAtLeft", typeof(double), typeof(CreepLine), new PropertyMetadata(0.0));
		public double EndContentAtLeft
		{
			get { return (double)GetValue(EndContentAtLeftProperty); }
			protected set { SetValue(EndContentAtLeftProperty, value); }
		}
		public static readonly DependencyProperty EndContentAtRightProperty = DependencyProperty.Register("EndContentAtRight", typeof(double), typeof(CreepLine), new PropertyMetadata(0.0));
		public double EndContentAtRight
		{
			get { return (double)GetValue(EndContentAtRightProperty); }
			protected set { SetValue(EndContentAtRightProperty, value); }
		}
		public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration", typeof(Duration), typeof(CreepLine), new PropertyMetadata(Duration.Automatic));
		public Duration Duration
		{
			get { return (Duration)GetValue(DurationProperty); }
			protected set { SetValue(DurationProperty, value); }
		}

		public static readonly DependencyProperty PixelsPerSecondSpeedProperty = DependencyProperty.Register("PixelsPerSecondSpeed", typeof(double), typeof(CreepLine), new PropertyMetadata(75.0, PixelsPerSecondSpeedChanged));
		private static void PixelsPerSecondSpeedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			((CreepLine)sender).OnPixelsPerSecondSpeedChanged((double)e.OldValue, (double)e.NewValue);
		}
		public double PixelsPerSecondSpeed
		{
			get { return (double)GetValue(PixelsPerSecondSpeedProperty); }
			set { SetValue(PixelsPerSecondSpeedProperty, value); }
		}
		protected virtual void OnPixelsPerSecondSpeedChanged(double oldValue, double newValue)
		{
			if (newValue < 1)
				PixelsPerSecondSpeed = 1.0;
			else
				CalculateDuration();
		}

		public static readonly DependencyProperty InitialDelayProperty = DependencyProperty.Register("InitialDelay", typeof(TimeSpan), typeof(CreepLine), new PropertyMetadata(TimeSpan.Zero, InitialDelayChanged));
		private static void InitialDelayChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			((CreepLine)sender).OnInitialDelayChanged((TimeSpan)e.OldValue, (TimeSpan)e.NewValue);
		}
		public TimeSpan InitialDelay
		{
			get { return (TimeSpan)GetValue(InitialDelayProperty); }
			set { SetValue(InitialDelayProperty, value); }
		}
		protected virtual void OnInitialDelayChanged(TimeSpan oldValue, TimeSpan newValue)
		{
			delayTimer.Interval = TimeSpan.FromMilliseconds(Math.Max(1.0, newValue.TotalMilliseconds));
		}
	}
}
