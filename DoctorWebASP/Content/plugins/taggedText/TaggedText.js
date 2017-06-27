jQuery.fn.extend({
  taggedText: function(args){
  	
  	function placeCaretAtEnd(el) {
	    el.focus();
	    if (typeof window.getSelection != "undefined"
	            && typeof document.createRange != "undefined") {
	        var range = document.createRange();
	        range.selectNodeContents(el);
	        range.collapse(false);
	        var sel = window.getSelection();
	        sel.removeAllRanges();
	        sel.addRange(range);
	    } else if (typeof document.body.createTextRange != "undefined") {
	        var textRange = document.body.createTextRange();
	        textRange.moveToElementText(el);
	        textRange.collapse(false);
	        textRange.select();
	    }
	}

  	function buildTag(tagName){
		return "<span class='tag' contenteditable='false'>"+tagName+"</span>"	
	}

	function renderContentWithTags(select, apply){
		var content = $(select).html().split(/({{\w+}})/g);
		$.each(content, function(index, element){					
			if (/({{\w+}})/g.test(element))
			{
				content[index] = buildTag(element.substring(2, element.length-2));
			}
		});
		if (apply){
			$(select).html(content.join("").trim());
		}else{
			return content.join("").trim();
		}
	}

	function renderContentWithKeys(select, apply){
		var content = $(select).clone()
		var tags = content.find(".tag");				
		$.each(tags, function(index, element){					
			$(element).replaceWith("{{"+$(element).html()+"}}")
		});
		if (apply){
			$(select).html(content.html());
		}else{
			return content.html();
		}

	}
  	if (args && args.method && args.method == "getContentWithTags"){
  		var result = [];
  		this.each(function(){
		var taggedtext = $(this);
  			result.push(renderContentWithTags(taggedtext, false));
  		});
  		return result;
  	}
  	else if (args && args.method && args.method == "getContentWithKeys"){
  		var result = [];
  		this.each(function(){
		var taggedtext = $(this);
  			result.push(renderContentWithKeys(taggedtext, false));
  		});
  		return result;
  	}else if (args && args.method) {
  		console.log("TaggedText invalid method "+args.method+"...")
  		return[];
  	}
  	return this.each(function() {		    	
    	var KEY_OPEN = 222,
    		KEY_CLOSE = 191;
		var previousKey = 0,
			catchingKeys = false,
			renderTag = false;
		
		var taggedtext = $(this);

		taggedtext
		.unbind("keydown")
		.unbind("keyup")
		.keydown(function(event ){
		    if (previousKey == KEY_CLOSE && event.which == KEY_CLOSE)
		    {
				renderTag = true;
			}
			previousKey = event.which;
		})
		.keyup(function(event){
		    if (renderTag)
		    {
				renderContentWithTags(taggedtext, true);
				renderTag = false;
				catchingKeys = false;
				placeCaretAtEnd(document.getElementsByClassName("taggedtext")[0]);
			}
		})
		.on("focus", function(){
			renderContentWithTags(taggedtext, true);
		})
		.on("blur", function(){
			renderContentWithTags(taggedtext, true);
		})
		.removeClass("taggedtext")
		.addClass("taggedtext");
		renderContentWithTags(taggedtext, true);
	});
  }
});