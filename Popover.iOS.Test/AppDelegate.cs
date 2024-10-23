namespace Popover.iOS.Test;

[Register("AppDelegate")]
public class AppDelegate : UIApplicationDelegate, IUIPopoverPresentationControllerDelegate
{
	public override UIWindow? Window { get; set; }

	public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
	{
		// create a new window instance based on the screen size
		Window = new UIWindow(UIScreen.MainScreen.Bounds);

		// create a UIViewController with a single UILabel
		var vc = new UIViewController();
		var label = new UILabel(Window!.Frame)
		{
			Lines = 0,
			BackgroundColor = UIColor.SystemBackground,
			LineBreakMode = UILineBreakMode.WordWrap,
			TextAlignment = UITextAlignment.Center,
			AutoresizingMask = UIViewAutoresizing.All,
			Text =
				"Lorem ipsum odor amet, consectetuer adipiscing elit. Cursus in rutrum commodo ultrices; eleifend parturient hendrerit varius. Aliquam laoreet nam libero erat mus posuere. Varius cras erat leo platea; luctus platea laoreet. Imperdiet vehicula sociosqu laoreet ligula litora nec purus penatibus. Sem per est libero arcu nisi id. Massa mollis nam tortor vivamus vulputate maximus netus. Odio ipsum phasellus ac neque ipsum nam parturient, condimentum morbi. Id bibendum penatibus lobortis non magnis curae phasellus dolor. Ridiculus id donec egestas ridiculus vulputate nam vulputate?\n\nConvallis urna diam tempor augue facilisis himenaeos rhoncus dictum. Arcu quisque sollicitudin pretium lorem et morbi euismod ullamcorper. Varius proin sed phasellus varius tellus taciti enim eros donec. Cubilia erat taciti imperdiet, magna dis ornare. Iaculis diam ultrices nascetur maecenas cubilia ex pharetra. Ornare sagittis urna orci at ante nostra nisl feugiat. Purus laoreet ligula tempus vel cursus. Aenean torquent molestie aptent montes est fusce. Quam neque dictumst potenti hac curabitur porttitor lacus.\n\nRisus viverra magna vestibulum, elementum ex sodales fringilla commodo. Bibendum quam neque curae magnis iaculis id elementum leo. Aviverra auctor rhoncus curabitur, a interdum phasellus augue tempor. Etiam molestie parturient varius; vehicula augue primis. Morbi sodales gravida posuere venenatis vivamus erat id sollicitudin. Conubia diam viverra dictumst magnis conubia. Lacinia penatibus netus sem ridiculus quisque magna.\n\nPotenti nec porttitor neque vulputate inceptos sollicitudin suspendisse. Primis maximus sed mollis sollicitudin facilisi. Cubilia posuere suscipit dui arcu dictum. Iaculis lacus posuere blandit odio netus venenatis libero proin. Fermentum sagittis mus semper lacus netus tincidunt libero. Elit fusce vitae aenean, tincidunt vel nisl. At congue urna cubilia nascetur facilisis. Hendrerit nullam facilisi fermentum dolor; penatibus lobortis a hac. Adipiscing nullam habitasse sociosqu vulputate penatibus feugiat.\n\nConsequat elementum fermentum luctus accumsan felis platea mauris. Nam rutrum aliquet aptent dui sodales adipiscing. Vulputate mus nascetur per fusce finibus nascetur. Posuere duis porttitor ligula; magnis rutrum vel justo tortor. Vehicula maximus etiam tortor varius sem. Facilisi tempor laoreet nisl id finibus. Mi auctor adipiscing ante litora dignissim, morbi cras mus. Fermentum amet ullamcorper feugiat aliquet penatibus mi netus. Ex ultrices vehicula natoque augue sem quisque maecenas. Integer venenatis pellentesque maecenas maximus facilisi pretium ac purus."
		};
		vc.View!.AddSubview(label);
		
		var btnDefault = new UIButton();
		btnDefault.BackgroundColor = UIColor.Black;
		btnDefault.SetTitle("Show Default", UIControlState.Normal);
		btnDefault.TitleLabel.TextAlignment = UITextAlignment.Center;
		btnDefault.TouchUpInside += BtnDefault_OnTouchUpInside;
		vc.View.AddSubview(btnDefault);
		btnDefault.SizeToFit();
		btnDefault.Center = vc.View.ConvertPointFromView(vc.View.Center, vc.View.Superview);
		
		var btnCustom = new UIButton();
		btnCustom.BackgroundColor = UIColor.Black;
		btnCustom.SetTitle("Show Custom", UIControlState.Normal);
		btnCustom.TitleLabel.TextAlignment = UITextAlignment.Center;
		btnCustom.TouchUpInside += BtnCustom_OnTouchUpInside;
		vc.View.AddSubview(btnCustom);
		btnCustom.SizeToFit();
		btnCustom.Center = new CGPoint(vc.View.ConvertPointFromView(vc.View.Center, vc.View.Superview).X, btnDefault.Center.Y + btnCustom.Frame.Height + 8);
		
		Window.RootViewController = vc;
		
		// make the window visible
		Window.MakeKeyAndVisible();

		return true;
	}

	private void BtnDefault_OnTouchUpInside(object? sender, EventArgs e)
	{
		ShowPopover(sender as UIButton, false);
	}
	
	private void BtnCustom_OnTouchUpInside(object? sender, EventArgs e)
	{
		ShowPopover(sender as UIButton, true);
	}

	private void ShowPopover(UIView? sourceView, bool isCustom)
	{
		var vc = new UIViewController();

		if (vc.View is null || sourceView is null)
		{
			return;
		}
		
		vc.View.BackgroundColor = UIColor.Red.ColorWithAlpha(0.5f);
		
		if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
		{
			vc.ModalPresentationStyle = UIModalPresentationStyle.Popover;
			UIPopoverPresentationController ppc = vc.PopoverPresentationController ?? new UIPopoverPresentationController(vc, Window?.RootViewController);
			ppc.SourceView = sourceView;
			ppc.SourceRect = sourceView.Frame;
			ppc.PermittedArrowDirections = UIPopoverArrowDirection.Any;
			//ppc.BackgroundColor = UIColor.Clear;

			if (isCustom)
			{
				ppc.PopoverBackgroundViewType = typeof(PopoverBackgroundView);
			}

			//ppc.Delegate = this;
		}

		Window?.RootViewController?.PresentViewController(vc, true, null);
	}

	[Export("prepareForPopoverPresentation:")]
	public void PrepareForPopoverPresentation(UIPopoverPresentationController popover)
	{
	
	}
}