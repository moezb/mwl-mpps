// --file-- hcore.js
/*
 * Copyright (c) 2005, 2009 Justen Software LLC, also known as
 * justen.com and hiplatform.org. All rights reserved.
 * This material is made available under the terms of the HiPlatform V1
 * License. For more information, visit http://www.justen.com
 */

/* updated Mar 1, 2009

ADDED
- code refactored to provide methods to add DOM nodes 
  using DOM methods (instead of using 'document.write'); 
  lets calling controls work with ajax 
*/

var HCore_DYNAMIC = "dynamic";
var HCore_NATURAL = "natural";
	// note: HtmlMenuControl has reference to this 
if ( HCore_IMAGES_FOLDER == undefined )
	var HCore_IMAGES_FOLDER = "images";
	// your framework can set this before it includes this script
	// to make the other scripts get their images from somewhere 
	// other than the default of "./images". 
var HCore_SCRIPTS_WITH_NO_PARENT = "hcore_scripts_with_no_parent";

/** Singleton with common methods */
var HCore = new HCore_();
function HCore_()
	{
	this.debugOn = false;
	this.monthAbbreviations = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
	
	/* 
	 Argument is a string containing a date in the form yyyymmdd 
	 with optional trailing time value of hhmmxx or hhmmssxx, where 
	 xx is omitted or 'AM' or 'PM'. The return value is of the 
	 form 'dd-mmm-yyyy hh:mm:ss xx', for example '5-May-2004 08:30:21 AM'. 
	 The time portion is omitted if the time is missing from the input 
	 argument. Likewise, when a time is present, the seconds and the 
	 'AM' or 'PM' are included only when they are present in the input. 
	*/
	this.formatDate = function( v )
		{
		if ( v.length < 8 )
			return( v );
		var y = v.substring( 0, 4 );
		var m = v.substring( 4, 6 );
		var d = v.substring( 6, 8 );
		var date = (d*1) + '-' + this.monthAbbreviations[m-1] + '-' + y;
		if ( v.length == 8 )
			return( date );
		
		var h = v.substring( 8, 10 );
		var n = v.substring( 10, 12 );
		var time = (h*1) + ':' + n;
		if ( v.length == 12 )
			return( date + ' ' + time );
		var x = v.substring( 12, 14 );
		if ( x == 'AM' || x == 'PM' )	 
			return( date + ' ' + time + ' ' + x );
		time = time + ':' + x;
		if ( v.length == 12 )
			return( date + ' ' + time );
		return( date + ' ' + time + ' ' + v.substring( 14, v.length ) );
		}

	/*
	 Removes leading and trailing spaces.
	*/		
	this.trim = function( s )
		{
		return s.replace(/^\s+|\s+$/g,"");
		}
		
	this.serialNumber = 0;
	this.getNextSerialNumber = function()
		{
		this.serialNumber++;
		return( this.serialNumber );
		}

	this.write = function( data )
		{
		document.write( data );
		}

	this.isRightClick = function( mouseEvent )
		{
		var e = mouseEvent || window.event;
		var rightclick;
		if (e.which) rightclick = (e.which == 3);
		else if (e.button) rightclick = (e.button == 2);
		return rightclick;
		}
		
	this.getMousePosition = function( mouseEvent ) 
		{
		var e = mouseEvent || window.event;
		var xpos = 0;
		var ypos = 0;
		
		if ( e.pageX || e.pageY )
			{
			xpos = e.pageX;
			ypos = e.pageY;
			}
		else
			{	
			// hack around IE issue 
			if ( document.body != null )
				{ 
				xpos = e.clientX + 
					document.body.scrollLeft + 
					document.documentElement.scrollLeft;
				ypos = e.clientY + 
					document.body.scrollTop + 
					document.documentElement.scrollTop;
				}
			}
			
		var pos = {x:xpos,y:ypos};	
		return( pos );
		}

	/* returns o.x and o.y */
	this.getElementPosition = function( elmt )
		{
		var left = 0;
		var top  = 0;

		while ( elmt.offsetParent )
			{
			left += elmt.offsetLeft;
			top  += elmt.offsetTop;
			elmt = elmt.offsetParent;
			}

		left += elmt.offsetLeft;
		top  += elmt.offsetTop;
		
		return {x:left, y:top};
		}

	this.getMouseOffset = function( fromElmt, mouseEvent )
		{
		var e = mouseEvent || window.event;

		var docPos = this.getElementPosition( fromElmt );
		var mousePos = this.getMousePosition( e );
		
		return {x:mousePos.x - docPos.x, y:mousePos.y - docPos.y};
		}
		
	this.mouseInside = function( fromElmt, mouseEvent )
		{
		var o = this.getMouseOffset( fromElmt, mouseEvent );
		if ( o.x >= 0 && o.x < fromElmt.clientWidth &&
			o.y >= 0 && o.y < fromElmt.clientHeight )
			return( true );
		return( false );		
		}
		
	/** drag and drop support */	

	this.mouseMove = function( event )
		{
		// attached to the document object, so 'this' == document
		
		event = event || window.event;
		var mousePos = HCore.getMousePosition( event );

 		var o = HCore.dragObject;
		if ( o != null )
			{
			var l = o.H_dragLimits;
			var insideLimits = true;
			
			// if they gave limits, don't let them drag it outside them
			if ( l != null )
				{
				if ( o.H_inXDirection &&
					( mousePos.x < l.left || mousePos.x > l.right ) )
					insideLimits = false;
				if ( o.H_inYDirection &&
					( mousePos.y < l.top || mousePos.y > l.bottom )	)
					insideLimits = false;
				}
				
			// otherwise, don't let them drag it outside the window
			else
				{
				var ws = HCore.getWindowSize();
				if ( o.H_inXDirection &&
					( mousePos.x < 0 || mousePos.x > ws.width ) )
					insideLimits = false;
				if ( o.H_inYDirection &&
					( mousePos.y < 0 || mousePos.y > ws.height ) )
					insideLimits = false;
				}
			
			if ( insideLimits )
				{
				document.body.style.cursor = o.H_cursor;
				if ( o.H_dragging != undefined )
					o.H_dragging( event );
				}
			else
				document.body.style.cursor = 'default';

			return false;
			}
		}
		
	this.mouseUp = function( mouseEvent )
		{
		// attached to the document object, so 'this' == document
		
		var e = mouseEvent || window.event;
		var o = HCore.dragObject;
		if ( o == null )
			return;
		if ( o.H_dropped != undefined )
			// if a callback has been defined
			o.H_dropped( e );
		document.body.style.cursor = 'default';			
		HCore.dragObject = null;
		}
		
	document.onmousemove = this.mouseMove;
	document.onmouseup = this.mouseUp;
	this.dragObject = null;

	this.attachDragHandler = function( 
		elmt, inXDirection, inYDirection, limits )
		{
		elmt.H_inXDirection = inXDirection;
		elmt.H_inYDirection = inYDirection;
		elmt.H_dragLimits = limits;
		
		if ( inXDirection && inYDirection )
			elmt.H_cursor = 'move';
		else
			{
			if ( inXDirection )
				elmt.H_cursor = 'e-resize';
			if ( inYDirection )
				elmt.H_cursor = 'n_resize';
			}
		elmt.onmouseover = function( event )
			{
			this.style.cursor = this.H_cursor;
			}
		elmt.onmouseout = function( event )
			{
			this.style.cursor = 'default';
			if ( this.H_onmouseout != undefined )
				// if a callback has been defined
				this.H_onmouseout( event );
			}
		elmt.onmousedown = function( event )
			{
			event = event || window.event;
			HCore.dragObject = this;
			document.body.style.cursor = this.H_cursor;
			if ( this.H_dragStarted != undefined )
				// if a callback has been defined
				this.H_dragStarted( event );
			return false;
			}
		}				   

	this.formatNumber = function(num,dec,thou,pnt,curr1,curr2,n1,n2) 
		{
		// number formatting function
		// copyright Stephen Chapman 24th March 2006
		// permission to use this function is granted provided
		// that this copyright notice is retained intact
		var x = Math.round(num * Math.pow(10,dec));
		if (x >= 0) n1=n2='';
		var y = (''+Math.abs(x)).split('');
		var z = y.length - dec;
		y.splice(z, 0, pnt);
		while (z > 3) 
			{z-=3;
			y.splice(z,0,thou);
			}
		var r = curr1+n1+y.join('')+n2+curr2;
		return r;
		}
		
	this.formatUsDollars = function( value, decimals )
		{
		var decimalPt = '.';
		if ( decimals == 0 )
			decimalPt = '';
		return this.formatNumber( value, decimals, ',', decimalPt, '$', '','-','');
		} 

	this.controls = [];
	this.shuntResizing = false;
	/* 
	Lets controls implement methods that get called when 
	window has finished loading or has been resized.
	*/
	this.addWindowListener = function( control )
		{
		this.controls.push( control );

/*		
		if ( window.onload == undefined )
			{
			window.onload = function()
				{
				for ( var n = 0; n < HCore.controls.length; n++ )
					{
					var ctl = HCore.controls[n];
					if ( ctl.onload != undefined )
						ctl.onload();
					}
				H_onload();
				}
			}
			
		if ( window.onresize == undefined )
			{
			window.onresize = function()
				{
				HCore.initiateResize();
				}
			}
*/
		}
		
	// install global window onload and resize handlers	
		
	var HCore_saveOnloadHandler = null
	if ( window.onload != undefined )
		HCore_saveOnloadHandler = window.onload;
	window.onload = function()
		{
		for ( var n = 0; n < HCore.controls.length; n++ )
			{
			var ctl = HCore.controls[n];
			if ( ctl.onload != undefined )
				ctl.onload();
			}
		if ( HCore_saveOnloadHandler != null )
			HCore_saveOnloadHandler();
		}
			
	var HCore_saveResizeHandler = null
	if ( window.onresize != undefined )
		HCore_saveResizeHandler = window.onresize;
	window.onresize = function()
		{
		HCore.initiateResize();
		if ( HCore_saveResizeHandler != null )
			HCore_saveResizeHandler();
		}

	this.initiateResize = function()
		{
		// IE 6 floods this with a huge number of messages
		// when page is made smaller, so we use the artifice
		// of a delayed job to filter out most of them. This is
		// less accurate and makes the Firefox version less
		// responsive but it avoids CPU meltdown in IE 6. It also
		// avoids a situation wherein IE 6 will issue another
		// resize message when the controls are resized inside
		// this logic (producing an infinite loop).
		if ( HCore.shuntResizing )
			return;
		HCore.shuntResizing = true;	
		
/*
var wsize = HCore.getWindowSize();			
alert( " window height " + wsize.height +  
	" window width " + wsize.width +  
	" body height " + document.body.clientHeight );
*/		
		HCore.makeControlsVisible( false );
		
		for ( var n = 0; n < self.frames.length; n++ )
			{
		    // experimental fix for resizing iframe when window resized
			// does not work on Firefox; works *most* of the time on IE 6
			if ( self.frames[n].HCore )
				self.frames[n].HCore.initiateResize();
			}				
		
		setTimeout( "HCore_doResizing()", 250 );
			// wait a while and then do the actual resize
		}

	this.makeControlsVisible = function( on )
		{
		for ( var n = 0; n < this.controls.length; n++ )
			{
			var ctl = this.controls[n];
			if ( ctl.setVisible != undefined )
				ctl.setVisible( on );
			}
		}

	this.keyControls = [];
	/* 
	Lets controls implement methods that get called when 
	a key is pressed.
	*/
	this.addKeyListener = function( control )
		{
		this.keyControls.push( control );
		
		if ( document.onkeyup == undefined )
			{
			document.onkeyup = function( event )
				{
				if ( !event ) var event = window.event;
					// standard cross browser event logic
				for ( var n = 0; n < HCore.keyControls.length; n++ )
					{
					var ctl = HCore.keyControls[n];
					if ( ctl.onkeyup != undefined )
						{
						var keyConsumed = ctl.onkeyup( event );
						if ( keyConsumed )
							break; 
						}
					}
				}
			}
		}		
		
	this.getWindowSize = function()
		{
		var w = 0;
		var h = 0;
		if( typeof( window.innerWidth ) == 'number' )
			{
			w = window.innerWidth;
			h = window.innerHeight;
			}
		else 
			{
		  	w = document.documentElement.clientWidth;
		  	h = document.documentElement.clientHeight;
			}

		return { width:w, height:h }
		}
		
	this.popups = [];
	this.nextPopup = 1;
	/* 
	 * Returns Window object for new popup. Pass -1 for 
	 * top, left, width, and/or height to not specify that value.
	 * The modal argument should be true to make popup close 
	 * if user focuses parent.  
	 */
	this.showPopupWindow = function( 
		url, name, left, top, width, height, includeMenu, modal )
		{
		// generate a name if caller did not give one
		if ( name == null )
			{
			this.nextPopup++;
			name = 'popup' + this.nextPopup;
			}
			
		var arg = "toolbar=0,scrollbars=1,location=0,status=0,menubar=";
		if ( includeMenu )
			arg += '1';
		else 
			arg += '0'; 
		arg += ",resizable=1";
		if ( width != -1 )
			arg += ",width=" + width;
		if ( height != -1 )
			arg += ",height=" + height;
		if ( top != -1 )
			arg += ",top=" + top;
		if ( left != -1 )
			arg += ",left=" + left;
			
		var w = window.open( url, name, arg, true );
			/* if window already exists, content replaced with new content */
			
//alert( "back on main window" );			
		if ( window.focus )
			{
			w.focus();
			}
		this.popups.push( {name:name, window:w, modal:modal} );
		return w;
		}

	/** 
	 * Closes a popup window. The x argument can either be 
	 * the name of the window or the window object itself.
	 */
	this.hidePopupWindow = function( x )
		{
		var p = this.popups;
		
		for ( var n = 0; n < p.length; n++ )
			{
			if ( p[n].name == x || p[n].window == x )
				{
				p[n].window.close();
				p.splice( n, 1 );
				return;
				}
			if ( p[n].closed )
				// in case user code closes a popup 
				p.splice( n, 1 );
			}  
		}
			
	window.onfocus = function()
		{
		var p = HCore.popups;
		var pVisible = [];
		for ( var n = 0; n < p.length; n++ )
			{
			if ( p[n].modal )
				p[n].window.close();
			else
				pVisible.push( p[n] );
			}
		HCore.popups = pVisible;
		}
		
	// DOM manipulation methods
	
	this.setRoot = function( id )
		{
		var e = null;
		if ( id ) 
			e = document.getElementById( id );
		if ( e == null )
			{
			// typically occurs with popup menus.
			// tried: e = document.body; but this generates an
			// "Operation aborted" error in IE6, which per the MS doc
			// http://support.microsoft.com/default.aspx/kb/927917
			// this is caused by changing a container whose parent
			// (like document.body) is 'open'. Tried adding a new
			// node and putting it in that, but IE6 seems to require
			// an existing node. So, design ASSUMES Html programmer
			// has an element so named:
			
			id = HCore_SCRIPTS_WITH_NO_PARENT;
			e = document.getElementById( id );
			if ( e == null )
				alert( 'Html needs an element with an id="' + 
					HCore_SCRIPTS_WITH_NO_PARENT + '"' );
			}			
		this.nodes = [];
		this.nodes.push( e );
		}
		
	// attr is 'a="b" c="d" ...'; attr name and value cannot have spaces
	this.startChild = function( tagName, attr )
		{
		var e = document.createElement( tagName );
		if ( e.tagName.toLowerCase() == 'table' )
			e.appendChild( document.createElement( 'tbody' ) );
		
		var parent = this.getCurrentNode();
		if ( parent.tagName.toLowerCase() == 'table' )
			parent.tBodies[0].appendChild( e );
		else
			parent.appendChild( e );
		this.nodes.push( e );
this.debug( '+ ' + tagName );
		
		if ( attr )
			{
			// attr is 'a="b" c="d" ...'
			//var tok = attr.split( ' ' );
			var tok = [];
			var insideQuote = false;
			var curTok = '';
			for ( var n = 0; n < attr.length; n++ )
				{
				var c = attr.charAt( n );
				if ( c == '"' )
					insideQuote = !insideQuote;
				if ( c == ' ' && !insideQuote )
					{
					if ( curTok.length > 0 )
						tok.push( curTok );
					curTok = '';
					}
				else
					curTok += '' + c;
				}
			if ( curTok.length > 0 )
				tok.push( curTok );
			
			for ( var n = 0; n < tok.length; n++ )
				{
				var nv = tok[n].split( '=' );
				if ( nv.length == 1 )				
					// skip empty tokens (caused by consecutive blank chars)
				continue;
//alert( tagName + ' ' + tok[n] + ' ' + nv[0] + ' ' + nv.length	);
				var name = nv[0];
				var value = nv[1];
				value = value.substring( 1, value.length - 1 );
				
				// class 
				if ( name == 'class' )
					{
					e.className = value;
					continue;
					}
				if ( name == 'style' )
					{
					this.setStyle( e, value );
					continue;
					}
					
				// IE 6 needs this
				if ( name == 'cellpadding' )
					name = 'cellPadding';
				if ( name == 'cellspacing' )
					name = 'cellSpacing';
				if ( name == 'colspan' )
					name = 'colSpan';
				if ( name == 'rowspan' )
					name = 'rowSpan';
				if ( name == 'valign' )
					name = 'vAlign';
				if ( name == 'bgcolor' )
					name = 'bgColor';
//				if ( name == 'nowrap' )
//					name = 'noWrap';
				
				e.setAttribute( name, value );
var test = name + ' = ' + value;
this.debug( '  ' + test );
				}
			}
			
		return e;			
		}
		
	this.setStyle = function( e, style )
		{
		var prop = style.split( "\;" );
		for ( var n = 0; n < prop.length; n++ )
			{
			var nameAndValue = prop[n].split( "\:" );
			if ( nameAndValue.length != 2 )
				continue;
			var name = HCore.trim( nameAndValue[0] );
			var value = HCore.trim( nameAndValue[1] );
			
			var nameJs = '';
			for ( var m = 0; m < name.length; m++ )
				{
				if ( name.charAt( m ) == '-' && m < name.length -1 )
					{
					nameJs += name.substring( m+1, m+2 ).toUpperCase();
					m++;
					}
				else
					nameJs += '' + name.charAt( m );
				}
			
this.debug( nameJs + ' = ' + value );
			try
				{
				e.style[nameJs] = value;
				}
			catch( err )
				{
				//alert( 'HCore:660 style property ' + nameJs + ':' + value + ' wont work:' + err.description );
				}
			}
		}
		
	this.addContent = function( html )
		{
		if ( html == '&nbsp;' )
			html = ' ';
		var h = this.getCurrentNode();
		h.appendChild( document.createTextNode( html ) );
		}
		
	this.endChild = function()
		{
		var e = this.nodes.pop();
this.debug( '- ' + e.tagName );		
		} 
	
	this.getCurrentNode = function()
		{
		return this.nodes[this.nodes.length-1];
		}
		
	this.setContent = function( elementId, html )
		{
		var h = html;
		var scripts = [];
		while( true )
			{
			var beg = h.indexOf( '<scr' + 'ipt>' );
				// must break this up or js interpreter foo's
			if ( beg == -1 )
				break;
			
			var s = h.substring( beg+8 );
			var end = h.indexOf( '</scr' + 'ipt>' );
			s = h.substring( beg+8, end );
			h = h.substring( 0, beg ) + h.substring( end+9 );
			
			scripts.push( s );
			}
			
		var e = document.getElementById( elementId );
	   	e.innerHTML = null;
	   	e.innerHTML = h;
		
		for ( var n = 0; n < scripts.length; n++ )
			{ 
	   		var s = document.createElement('script');
	   		s.type = "text/javascript";
			s.text = scripts[n];
			var head = document.getElementsByTagName("head")[0];
			e.appendChild( s );
			}
		}	
		
	this.debug = function( msg )
		{
		if ( this.debugOn )
			document.write( msg + "<br>" );
		}	
	}

/** standalone method to make setTimeout work */
function HCore_doResizing()
	{
	for ( var n = 0; n < HCore.controls.length; n++ )
		{
		var ctl = HCore.controls[n];
		if ( ctl.onresize != undefined )
			ctl.onresize();
		}
		
	HCore.makeControlsVisible( true );
	
	for ( var n = 0; n < HCore.controls.length; n++ )
		{
		var ctl = HCore.controls[n];
		if ( ctl.onresizeCompleted != undefined )
			ctl.onresizeCompleted();
		}
		
	setTimeout( "HCore_resumeProcessingResizeMessages()", 500 );
	}
	

function HCore_resumeProcessingResizeMessages()
	{		
	HCore.shuntResizing = false;
	}
