/// <reference path="typings/jquery/jquery.d.ts" />
var Zaggoware;
(function (Zaggoware) {
    var Tagging;
    (function (Tagging) {
        var Tagger = (function () {
            function Tagger($container) {
                this.$container = $container;
                this.tagList = new TagList(this);
                if ($container != null) {
                    this.$textBox = $container.find("input[type=text]");
                    this.$tagList = $container.find("ul.tag-list");
                }
            }
            Tagger.initialize = function () {
                Tagger.taggers = new Array();
                $(".tag-list-container").each(function () {
                    Tagger.taggers.push(new Tagger($(this)));
                });
            };
            Tagger.prototype.getTagList = function () {
                return this.tagList;
            };
            Tagger.prototype.getContainer = function () {
                return this.$container;
            };
            Tagger.prototype.getFieldName = function () {
                return this.$textBox.data("name");
            };
            Tagger.prototype.render = function () {
                this.$container = $("<div/>").addClass("tag-list-container");
                this.tagList.render();
                this.addEventHandlers();
            };
            Tagger.prototype.addEventHandlers = function () {
            };
            return Tagger;
        })();
        Tagging.Tagger = Tagger;
        var TagList = (function () {
            function TagList(tagger) {
                this.tagger = tagger;
                this.items = new Array();
            }
            TagList.prototype.getTagger = function () {
                return this.tagger;
            };
            TagList.prototype.addNewItem = function (label, hiddenValue) {
                var item = new TagListItem(this, label, hiddenValue);
                return this.addItem(item);
            };
            TagList.prototype.addItem = function (item) {
                this.items.push(item);
                return item;
            };
            TagList.prototype.removeItem = function (item) {
                var index = this.indexOf(item);
                if (index !== -1) {
                    this.items.splice(index, 1);
                }
                item.remove(false);
            };
            TagList.prototype.indexOf = function (item) {
                return $.inArray(item, this.items);
            };
            TagList.prototype.render = function () {
                if (this.$container != null) {
                    this.$container.remove();
                }
                this.$container = $("<ul/>").addClass("tag-list").appendTo(this.tagger.getContainer());
                for (var i = 0; i < this.items.length; i++) {
                    this.items[i].render();
                }
            };
            TagList.prototype.getContainer = function () {
                if (this.$container == null) {
                    this.render();
                }
                return this.$container;
            };
            return TagList;
        })();
        var TagListItem = (function () {
            function TagListItem(tagList, label, hiddenValue) {
                this.tagList = tagList;
                this.label = label;
                this.hiddenValue = hiddenValue;
            }
            TagListItem.prototype.getLabel = function () {
                return this.label != null ? this.label : (this.hiddenValue != null ? this.hiddenValue.toString() : '');
            };
            TagListItem.prototype.getHiddenValue = function () {
                return this.hiddenValue;
            };
            TagListItem.prototype.render = function () {
                if (this.$container != null) {
                    this.$container.remove();
                }
                this.$container = $("<li/>").addClass("tag-list-item");
                this.$label = $("<span/>").addClass("tag-list-item-label").text(this.getLabel()).appendTo(this.$container);
                this.$removeButton = $("<span/>").addClass("tag-list-item-remove").text("X").appendTo(this.$label);
                var fieldName = this.tagList.getTagger().getFieldName();
                var index = this.tagList.indexOf(this);
                if (index < 0) {
                    index = 0;
                }
                $("<input/>").attr("type", "hidden").attr("name", fieldName + "[" + index + "].Label").val(this.getLabel()).appendTo(this.$container);
                $("<input/>").attr("type", "hidden").attr("name", fieldName + "[" + index + "].HiddenValue").attr("value", this.getHiddenValue()).appendTo(this.$container);
                this.tagList.getContainer().append(this.$container);
                this.addEventHandlers();
            };
            TagListItem.prototype.remove = function (removeFromTagList) {
                if (removeFromTagList === void 0) { removeFromTagList = true; }
                if (removeFromTagList) {
                    this.tagList.removeItem(this);
                }
                if (this.$container != null) {
                    this.$container.remove();
                    this.$container = null;
                }
            };
            TagListItem.prototype.addEventHandlers = function () {
                var _this = this;
                this.$container.find(".tag-list-item-remove").click(function () {
                    _this.remove();
                });
            };
            return TagListItem;
        })();
    })(Tagging = Zaggoware.Tagging || (Zaggoware.Tagging = {}));
})(Zaggoware || (Zaggoware = {}));
//# sourceMappingURL=Tagger.js.map