package crc6499cc3f8d6dc23bc6;


public class HyperlinkButton
	extends crc641cde71c42eef4307.ButtonBase
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Microsoft.UI.Xaml.Controls.HyperlinkButton, Uno.UI", HyperlinkButton.class, __md_methods);
	}


	public HyperlinkButton (android.content.Context p0)
	{
		super (p0);
		if (getClass () == HyperlinkButton.class)
			mono.android.TypeManager.Activate ("Microsoft.UI.Xaml.Controls.HyperlinkButton, Uno.UI", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
