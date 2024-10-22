using ObjCRuntime;

namespace Popover.iOS.Test;

public class PopoverBackgroundView : UIPopoverBackgroundView
{
	public PopoverBackgroundView(NativeHandle handle) : base(handle)
	{
	}
	
	[Export ("contentViewInsets")]
	public new static UIEdgeInsets GetContentViewInsets()
	{
		return new UIEdgeInsets(25, 25, 25, 25);
	}

	[Export("arrowHeight")]
	public new static nfloat GetArrowHeight()
	{
		return 25f;
	}

	public override UIPopoverArrowDirection ArrowDirection { get; set; }
	
	public override nfloat ArrowOffset { get; set; }
}