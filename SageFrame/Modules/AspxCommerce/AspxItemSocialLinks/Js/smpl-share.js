/*!
 * jQuery Simple Share
 * Very simple (extensiable) sharing buttons boilerplate for developers, supports: Facebook, Twitter, Google+ and Linkedin.
 * version 1.0, March 13th, 2014
 * by Ammar Alakkad - http://aalakkad.github.io
 * https://github.com/AAlakkad/jQuery-Smpl-Share
 */

;
(function ($) {

	$.smplShare = function (element, options) {

		var defaults = {
			sharing_urls: {
				googleplus: {
					width: 900,
					height: 500,
					url: 'https://plus.google.com/share?url=',
					params: {
						lang: 'en'
					}
				},
				facebook: {
					width: 900,
					height: 500,
					url: 'http://www.facebook.com/sharer/sharer.php?p[url]=',
					params: {
						text: 'p[title]'
					}
				},
				twitter: {
					width: 650,
					height: 360,
					url: 'https://twitter.com/intent/tweet?url=',
					params: {
						text: 'text',
						via: 'via'
					}
				},
				linkedin: {
					width: 550,
					height: 550,
					url: 'https://www.linkedin.com/cws/share?url='
				}
			},
			lang: 'en',
			services: ['twitter', 'facebook', 'googleplus', 'linkedin'],
			onClick: function () {
			}
		}

		var plugin = this;

		plugin.settings = {}

		var $element = $(element),
			element = element;

		plugin.init = function () {
			plugin.settings = $.extend({}, defaults, options);

			plugin.settings.url = $element.data('url') ? $element.data('url') : plugin.settings.url;
			plugin.settings.text = $element.data('text') ? $element.data('text') : plugin.settings.text;
			plugin.settings.via = $element.data('via') ? $element.data('via') : plugin.settings.via;
			plugin.settings.lang = $element.data('lang') ? $element.data('lang') : plugin.settings.lang;
			
			// If no url available, get the current url
			if (!plugin.settings.url)
				plugin.settings.url = window.location.protocol + "//" + window.location.host + "/" + window.location.pathname;

			plugin.settings.url = encodeURIComponent(plugin.settings.url);
			plugin.settings.text = encodeURIComponent(plugin.settings.text);
			plugin.settings.summary = encodeURIComponent(plugin.settings.summary);
			plugin.settings.via = encodeURIComponent(plugin.settings.via);

			$element.find(".item").each(function () {
				var itemClasses, itemService;
				// remove array item: 'item'
				itemClasses = $(this).attr('class').replace("item", "").trim().split(" ");

				// check if array contain on of the plugin.settings.services
				itemService = checkService(itemClasses[0]);
				// if so, bind on click to window.open
				var service, url;
				if (itemService) {
					$(this).click(function () {
						// get service_url
						service = plugin.settings.sharing_urls[itemService];
						url = service.url + plugin.settings.url;

						// add parameters to url
						if (service.params && Object.keys(service.params).length >= 1) {
							$.each(service.params, function (index, value) {
								if (plugin.settings[index]) {
									url += "&" + value + "=" + plugin.settings[index];
								}
							});
						}
						// Open sharing dialog
						window.open(url, "", "toolbar=0, status=0, width=" + service.width + ", height=" + service.height);
					});
				}
			});
		}

		// check if classes parameter contain any of settings.services
		var checkService = function (itemClass) {
			if (plugin.settings.services.indexOf(itemClass) != -1)
				return itemClass;
			return false;
		}

		plugin.init();

	}

	$.fn.smplShare = function (options) {

		return this.each(function () {
			if (undefined == $(this).data('smplShare')) {
				var plugin = new $.smplShare(this, options);
				$(this).data('smplShare', plugin);
			}
		});

	}

})(jQuery);
