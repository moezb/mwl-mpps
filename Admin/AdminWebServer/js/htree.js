// --file-- htree.js
/*
 * Copyright (c) 2005, 2009 Justen Software LLC, also known as
 * justen.com and hiplatform.org. All rights reserved.
 * This material is made available under the terms of the HiPlatform V1
 * License. For more information, visit http://www.justen.com
 */

/* updated Mar 1, 2009

ADDED
- code refactored to add DOM nodes using DOM methods (instead of 
  using 'document.write'); lets control work with ajax 
*/
 
/* updated August 20, 2007		

TODO: 
- sizeTree assumes (in width-2 expression a 1 pixel border)

*/

var HTree_EMPTY_PIXEL_IMAGE = HCore_IMAGES_FOLDER + '/misc_empty_pixel.gif';
var HTree_EXPANDER_IMAGE = HCore_IMAGES_FOLDER + '/tree_expander.gif';
var HTree_CONTRACTER_IMAGE = HCore_IMAGES_FOLDER + '/tree_contracter.gif';
var HTree_NOEXPANDER_IMAGE = HCore_IMAGES_FOLDER + '/tree_noexpander.gif';
var HTree_FOLDER_IMAGE = HCore_IMAGES_FOLDER + '/tree_folder.gif';
var HTree_CURRENT_FOLDER_IMAGE = HCore_IMAGES_FOLDER + '/tree_folder_current.gif';
var HTree_FILE_IMAGE = HCore_IMAGES_FOLDER + '/tree_file.gif';
var HTree_SCROLLBARS_ALWAYS = "always";
var HTree_SCROLLBARS_NEVER = "never";
var HTree_FOLDER_INDENT = 17;
	// should be same as width of expander + its right margin

/** static method used to convert data into HTreeNode objects */	
function HTree_makeNodes( data )
	{
	var a = [];
	for ( var n = 0; n < data.length; n++ )
		{
		a[n] = new HTreeNode( data[n] );
		}
	return a;
	}

/* Data model for one node in tree */
function HTreeNode( data )
	{
	// default and save constructor data
	this.data = data;
	if ( data.expanded == undefined )
		this.data.expanded = false;
	if ( data.current == undefined )
		this.data.current = false; 
	if ( data.children != undefined )
		this.data.children = HTree_makeNodes( data.children );
	
	this.getText = function() {	return this.data.text; }
	this.isExpanded = function() { return this.data.expanded; }
	this.isCurrent = function() { return this.data.current; }
	this.isLeaf = function() { return ( this.data.children == undefined ); }
	this.getChildren = function() { return this.data.children; }
	
	this.setExpanded = function( state ) 
		{
		this.data.expanded = state;
		}
	this.setCurrent = function( state ) { this.data.current = state; }
	
	this.getIcon = function()
		{
		if ( this.data.icon != undefined )
			return( this.data.icon );
			
		if ( this.isLeaf() )
			return HTree_FILE_IMAGE;
			
		else
			{
			if ( this.isCurrent() )
				return HTree_CURRENT_FOLDER_IMAGE;
			else
				return HTree_FOLDER_IMAGE;
			}
		}
	}

/* 
 * Data model for tree. The data argument is an array of objects 
 * that have the following properties:
 * <br>
 * text - the text shown for the node
 * <br>
 * expanded - a boolean indicating for nodes that have children, 
 * whether the node is expanded or not (when the tree is initially 
 * displayed).
 * <br>
 * icon - the name of an image to use for the node; if omitted, 
 * this defaults to a folder for nodes that have children and a 
 * document for nodes that don't have children
 * <br>
 * children - an array of the same kind of objects that define
 * the child nodes of this node (each of which may have their own
 * children property). 
 */
function HTreeModel( data )
	{
	this.data = HTree_makeNodes( data );
	
	/* returns [] of HTreeNode objects that are the root nodes in the tree */
	this.getRoots = function()
		{
		return this.data;
		}
	}

/*
 * Tree control. The model argument should be an instance of 
 * HTreeModel (or an object that supports the same methods).
 * The options argument can have properties:
 * <br>
 * visibleWidth - the width in pixels or HCore_DYNAMIC to use the width of 
 * the HTML element that contains the tree.
 * <br>
 * visibleHeight - the height in pixels or HCore_DYNAMIC to use the height of
 * the HTML element that contains the tree.
 * <br>
 * scroll - either HTree_SCROLLBARS_ALWAYS to show scrollbars as needed,
 * or HTree_SCROLLBARS_NEVER to avoid using scrollbars. 
 * <br>
 * To handle double clicks, create an HTree object 
 * and then set the handleDoubleClick and handleSelectionChange functions to 
 * the desired logic. For example:
 * <br>
 * 		var tree = new HTree( ... ); 
 * 		tree.handleDoubleClick = function( node ) { ... logic ... } 
 * <br>
 * See the 'aboutToExpandOrContractNode' method for information on 
 * fetching the tree's content dynamically. 
 */
function HTree( model, options )
	{
	this.model = model;	
	this.options = options;
		
	this.getModel = function() { return this.model; }
	
	/* 
	 * Returns a list of div tags (whose 
	 * HTree_node and HTree_level 
	 * attributes can be used to determine the 
	 * HTreeNode and level (from 0 as the root nodes). 
	 * The getLocation method can be used to get  
	 * the absolute index (line number from the 
	 * topmost line in the tree) of a div tag, 
	 */
	this.getSelection = function() { return this.selection; }
	
		
	// constructor (invoked at very end of class)
	this.init = function()
		{
		this.id = "HTree" + HCore.getNextSerialNumber();

		if ( this.options == null )
			this.options = {};
		this.defaultMissingOptions( options );
		this.selection = [];
		}
	
	this.defaultMissingOptions = function( options )
		{
		if ( options.visibleWidth == undefined )
			options.visibleWidth = HCore_DYNAMIC;
		if ( options.visibleHeight == undefined )
			options.visibleHeight = HCore_DYNAMIC;
		
		if ( options.scroll == undefined )
			options.scroll = HTree_SCROLLBARS_ALWAYS;
			// can be always or never
		if ( options.allowMultipleSelection == undefined )
			options.allowMultipleSelection = false;
		}
		
 	this.setRightClickMenu = function( menu, handler )
		{
		menu.render();
		this.rightClickMenu = menu;
		this.rightClickMenuHandler = handler;
		}

	this.render = function( parentNodeId )
		{
		HCore.setRoot( parentNodeId );
		var style = '';
		if ( this.options.scroll == HTree_SCROLLBARS_ALWAYS )
			style = ' style="overflow:auto"';
		HCore.startChild( 'div', 
			'id="' + this.id + '" class="htree-main"' + style );
		
		this.ctr = document.getElementById( this.id );
		this.currentMargin = 2;
		this.addNodes();
		
		HCore.addWindowListener( this );
			// causes onload() method to get called when page completely
			// loaded (and we know all the element sizes)
		}

	this.onload = function()
		{
		this.sizeTree();
		}

	this.onresize = function()
		{
		this.sizeTree();
		}
		
	this.setVisible = function( visible )
		{
		var e = document.getElementById( this.id );
		e.style.display = (visible ? "block" : "none" );
		}
		
	this.sizeTree = function()
		{
		var o = this.options;
		var e = document.getElementById( this.id );
		
		var width = -1;
		if ( o.visibleWidth == HCore_DYNAMIC )
			{
			if ( e.parentNode != null )
				width = e.parentNode.clientWidth;
			}
		else
			width = o.visibleWidth;
			
		var height = -1;
		if ( o.visibleHeight == HCore_DYNAMIC )
			{
			if ( e.parentNode != null )
				height = e.parentNode.clientHeight;
			}
		else
			height = o.visibleHeight;

		if ( width != -1 && width != e.style.width )
			e.style.width = (width-2) + "px";
			// -2 for the left and right border
		if ( height != -1 && height != e.style.height )
			e.style.height = (height-2) + "px";
			// -2 for the top and bottom border
		}
	
	this.addNodes = function()
		{
		var roots = this.model.getRoots();
		this.addChildNodes( 0, roots, 0 );
		}
		
	this.addChildNodes = function( location, children, level )
		{
		var num = children.length;
		for ( var n = 0; n < num; n++ )
			{
			var c = children[n];
			this.addNode( location, c, level );
			location++;
			if ( !c.isLeaf() && c.isExpanded() )
				{
				level++;
				location = this.addChildNodes( location, c.getChildren(), level );
				level--;
				}
			}
		return location;
		}
		
	this.addNode = function( location, node, level )
		{
		// create the node
		var div = document.createElement( "div" );
		div.style.whiteSpace = "nowrap";		
			// otherwise, narrow display area will cause text to wrap to next line
		div.HTree = this;
		div.HTree_node = node;
		div.HTree_level = level;
		node.div = div;		
		
		// add an image to indent the node to show its hierarchy
		var margin = level * HTree_FOLDER_INDENT;
		var img = document.createElement( "img" );
		img.style.width = margin;
		img.style.height = 1;
			// last two for Firefox
		img.width = margin;
		img.height = 1;
			// last two for IE
		img.src = HTree_EMPTY_PIXEL_IMAGE;
		this.setImgStyle( img );
		div.appendChild( img );
		
		// add an image to expand a folder node
		img = document.createElement( "img" );
		if ( node.isLeaf() )
			img.src = HTree_NOEXPANDER_IMAGE;
		else
			{
			if ( node.getChildren().length == 0 )
				img.src = HTree_NOEXPANDER_IMAGE;
			else
				{
				if ( !node.isExpanded() )
					img.src = HTree_EXPANDER_IMAGE;
				else
					img.src = HTree_CONTRACTER_IMAGE;
					
				this.attachExpanderListener( img );
				}
			}
		this.setImgStyle( img );
		img.style.marginRight = "1px";
		img.style.marginTop = "1px";
		div.HTree_expander = img;
		div.appendChild( img );
		
		// add an image to show a folder, open folder, or document (leaf)
		img = document.createElement( "img" );
		img.src = node.getIcon();
		this.setImgStyle( img );
		this.attachClickListener( img );
		this.attachDoubleClickListener( img );
		div.appendChild( img );
		
		var text = document.createElement( "div" );
		div.HTree_text = text;
		div.appendChild( text );
		var t = document.createTextNode( node.getText() );
		text.appendChild( t );
		text.className = "htree-text";
		this.attachClickListener( text );
		this.attachHoverListener( text, img );
		this.attachDoubleClickListener( text );
		
		// add the node to the tree
		var divs = this.ctr.childNodes;
		if ( divs.length == location )
			this.ctr.appendChild( div );
		else
			this.ctr.insertBefore( div, divs[location] );
		}
		
	this.setImgStyle = function( img )
		{
		img.style.verticalAlign = "middle";
		img.style.align = "left";
		img.style.display = "inline";
		}

	// add listener for +/- icons to expand or contract node		
	this.attachExpanderListener = function( img )
		{
		img.onclick = function()
			{
			var div = this.parentNode;
			var t = div.HTree;
			var node = div.HTree_node;
			
			if ( node.isExpanded() )
				t.setNodeExpanded( node, false );
			else
				t.setNodeExpanded( node, true );
			}
		}
		
	this.getLocation = function( div )
		{
		var divs = this.ctr.childNodes;
		for ( var n = 0; n < divs.length; n++ )
			{
			if ( divs[n] == div )
				return( n );
			} 
		return -1;
		}		
		
	this.attachClickListener = function( e )
		{
		e.oncontextmenu = function( event )
			{
			var div = this.parentNode;
			var t = div.HTree;
			
			if ( t.rightClickMenu != undefined )
				// so onclick will get called; otherwise, browser 
				// displays normal context menu and bypasses onclick
				{
				t.setSelection( div, false, false );
			
				var node = div.HTree_node;
				event = event || window.event;
					// standard idiom to handle IE event model
				t.handleRightClick( node, event );
				return false;
				}
			else
				return true;
			}
			
		e.onclick = function( event )
			{
			var div = this.parentNode;
			var t = div.HTree;
			event = event || window.event;
			
			var rightClick = false;
			if (event.which) 
				rightClick = (event.which == 3);
			else 
				{
				if ( event.button ) 
					rightclick = (event.button == 2);
				}
			
			if ( rightClick )
				{
				if ( t.rightClickMenu != undefined )
					{
					t.toggleSelectedNodes( true );
					t.selection = [];
					t.selection.push( div.HTree_node );
					t.toggleSelectedNodes( false );
					}
				return;
				}
			
			t.setSelection( div, event.ctrlKey, event.shiftKey );
			
			var node = div.HTree_node;
			if ( !node.isExpanded() && !node.isLeaf() )
				t.setNodeExpanded( node, true );
			}
		}
		
	this.expandNode = function( node )
		{
		var div = node.div;
		var level = div.HTree_level;
		var location = this.getLocation( div );
			
		node.setExpanded( true );
		
		this.addChildNodes( location+1, node.getChildren(), level+1 );
		var img = div.HTree_expander;
		if ( node.getChildren().length == 0 )
			img.src = HTree_NOEXPANDER_IMAGE;
		else
			img.src = HTree_CONTRACTER_IMAGE;
		}
		
	this.contractNode = function( node )
		{
		var div = node.div;
		var level = div.HTree_level;
		var location = this.getLocation( div );
			
		node.setExpanded( false );
		
		// remove the child nodes 
		var divs = this.ctr.childNodes;
		location++;
		while( location < divs.length )
			{
			var cdiv = divs[location];
			if ( cdiv.HTree_level <= level )
				break;
			this.ctr.removeChild( cdiv );
			}
			
		var img = div.HTree_expander;
		if ( node.getChildren().length == 0 )
			img.src = HTree_NOEXPANDER_IMAGE;
		else
			img.src = HTree_EXPANDER_IMAGE;
		}
		
	this.attachHoverListener = function( text, img )
		{
		text.onmouseover = function()
			{
			if ( this.className == "htree-text" )
				this.className = "htree-text-hover";
			if ( this.className == "htree-text-selected" )
				this.className = "htree-text-selected-hover";
			}
		text.onmouseout = function()
			{
			if ( this.className == "htree-text-hover" )
				this.className = "htree-text";
			if ( this.className == "htree-text-selected-hover" )
				this.className = "htree-text-selected";
			}
		img.HTree_text = text;
		img.onmouseover = function()
			{
			var t = this.HTree_text;
			if ( t.className == "htree-text" )
				t.className = "htree-text-hover";
			if ( t.className == "htree-text-selected" )
				t.className = "htree-text-selected-hover";
			}
		img.onmouseout = function()
			{
			var t = this.HTree_text;
			if ( t.className == "htree-text-hover" )
				t.className = "htree-text";
			if ( t.className == "htree-text-selected-hover" )
				t.className = "htree-text-selected";
			}
		}
		
	this.attachDoubleClickListener = function( nodeText )
		{
		nodeText.ondblclick = function()
			{
			var div = this.parentNode;
			var t = div.HTree;
			var node = div.HTree_node;
			t.handleDoubleClick( node ); 
			}
		}
		
	this.handleDoubleClick = function( node )
		{
		// no-op in base class; programmer reassigns function to instance specific logic
		return true;
		}
		
	this.handleRightClick = function( node, event )
		{
		pos = HCore.getMousePosition( event );
		this.rightClickMenu.showPopup( pos.x, pos.y, 
			this.rightClickMenuHandler, node );
		}
		
	this.setSelection = function( div, ctrl, shift )
		{
		if ( !shift && !ctrl )
			{
			this.toggleSelectedNodes( false );
			this.selection = [];
			}
		
		if ( !this.options.allowMultipleSelection || this.selection.length == 0 )
			{
			this.toggleSelectedNodes( false );
			this.selection = [];
			this.selection.push( div );
			this.toggleSelectedNodes( true );
			this.handleSelectionChange( div.HTree_node );
			return;
			}
			
		if ( !shift )
			{
			this.selection.push( div );
			this.toggleSelectedNodes( true );
			this.handleSelectionChange( div.HTree_node );
			return;
			}
			
		// find current selection closest to the div being selected
		var closestLocation = -1;
		var closestDelta = -1;
		var location = this.getLocation( div );
		var s = this.selection;
		for ( var n = 0; n < s.length; n++ )
			{
			var selectedLocation = this.getLocation( s[n] );
			var delta = location - selectedLocation;
			if ( delta < closestDelta || closestDelta == -1 )
				{
				closestDelta = delta;
				closestLocation = selectedLocation;
				}
			}
			
		// redo the selection to hold everyting between closest and current one
		s = [];
		var start = closestLocation;
		var end = location;
		if ( start > end )
			{
			var swap = start;
			start = end;
			end = swap;
			}
		for ( var n = start; n <= end; n++ )
			{
			s[n-start] = this.ctr.childNodes[n];
			}
		
		this.toggleSelectedNodes( false );
		this.selection = s;
		this.toggleSelectedNodes( true );
		this.handleSelectionChange( div.HTree_node );
		}
		
	this.toggleSelectedNodes = function( on )
		{
		for ( var n = 0; n < this.selection.length; n++ )
			{
			var e = this.selection[n].HTree_text;
			if ( on )
				{
				if ( e.className.indexOf( "hover" ) == -1 ) 
					e.className = "htree-text-selected";
				else
					e.className = "htree-text-selected-hover";
				}
			else
				{
				if ( e.className.indexOf( "hover" ) == -1 ) 
					e.className = "htree-text";
				else
					e.className = "htree-text-hover";
				}
			}
		}
		
	this.setNodeExpanded = function( node, expanded )
		{
		if ( this.aboutToExpandOrContractNode( node, expanded ) )
			return;
//alert( "htree.586: about to set node expanded" );			
		if ( expanded )
			this.expandNode( node );
		else
			this.contractNode( node );
		}
		
	/** 
	 * This method is called immediately before a node is expanded 
	 * (state is true) or contracted (state is false). If the method
	 * returns false, the default logic - which is to call the 
	 * expandNode() or contractNode() method - is performed. 
	 * Otherwise, the method is assumed to have adjusted 
	 * the tree to reflect the new expansion state. The default
	 * version of this method does nothing and returns false. 
	 * The default version can
	 * be overridden with custom logic to dynamically fetch or 
	 * discard the child nodes of the node about to be expanded 
	 * or contracted. Note: the logic must wrap new child objects in 
	 * HTreeNode objects. Typically, the function will create an 
	 * array of objects (having the properties described in the 
	 * comments for the HTreeModel class) and reset the  
	 * node's 'data.children' property:
	 * <br><br>
	 * var t = new HTree( ... 
	 * t.aboutToExpandOrContract = function( node, expanding )
	 * 		{
	 *		if ( expanding && treeNode.children.length == 0 )
	 *			// children initially defined with zero length array
	 *          // to allow dynamic expansion; avoid refetching children
	 *          // if they've already been read
	 *			{
	 *			var a = [];
	 *			// ... code to fetch children and populate 'a';
	 *			node.data.children = HTree_makeNodes( a );
	 *			}
	 *      return false;
	 *		}
	 * <br><br>
	 * The code to fetch the children and set node.data.children
	 * may run asynchronously (for example, using HiP's 
	 * H_sendSynchronousRequest function). In this case, the method
	 * should return true and the asynchronous code will include
	 * the logic that calls expandNode.    
	 */
	this.aboutToExpandOrContractNode = function( node, expanding )
		{
		return false;
		}
		
	this.handleSelectionChange = function( node )
		{
		// no-op in base class; programmer reassigns function to instance specific logic
		}
		
	this.init();
	}
