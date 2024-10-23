using ObjCRuntime;

namespace Popover.iOS.Test;

public class PopoverBackgroundView : UIPopoverBackgroundView, IUIPopoverBackgroundViewMethods
{
	private const float ARROW_BASE = 30.0f;
	private const float ARROW_HEIGHT = 20.0f;
	private const float BORDER_INSET = 8.0f;
	
	public PopoverBackgroundView(NativeHandle handle) : base(handle)
	{
	}
	
	[Export ("contentViewInsets")]
	public new static UIEdgeInsets GetContentViewInsets()
	{
		return new UIEdgeInsets(BORDER_INSET, BORDER_INSET, BORDER_INSET, BORDER_INSET);
	}

	[Export("arrowBase")]
	public new static nfloat GetArrowBase()
	{
		return ARROW_BASE;
	}
	
	[Export("arrowHeight")]
	public new static nfloat GetArrowHeight()
	{
		return ARROW_HEIGHT;
	}

	public nfloat CornerRadius { get; } = 5.0f;

	public override UIPopoverArrowDirection ArrowDirection { get; set; }
	
	public override nfloat ArrowOffset { get; set; }

	public override void LayoutSubviews()
	{
		base.LayoutSubviews();
		Layer.ShadowColor = UIColor.Clear.CGColor;
	}
}