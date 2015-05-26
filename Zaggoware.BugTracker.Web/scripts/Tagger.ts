/// <reference path="typings/jquery/jquery.d.ts" />

module Zaggoware.Tagging {
    export class Tagger {
        private static taggers: Array<Tagger>;

        private $textBox: JQuery;
        private $tagList: JQuery;

        private tagList: TagList;

        constructor(private $container?: JQuery) {
            this.tagList = new TagList(this);

            if ($container != null) {
                this.$textBox = $container.find("input[type=text]");
                this.$tagList = $container.find("ul.tag-list");
            }
        }

        public static initialize() {
            Tagger.taggers = new Array<Tagger>();

            $(".tag-list-container").each(function () {
                Tagger.taggers.push(new Tagger($(this)));
            });
        }

        public getTagList(): TagList {
            return this.tagList;
        }

        public getContainer(): JQuery {
            return this.$container;
        }

        public getFieldName() {
            return this.$textBox.data("name");
        }

        public render() {
            this.$container = $("<div/>")
                .addClass("tag-list-container");

            this.tagList.render();

            this.addEventHandlers();
        }

        private addEventHandlers() {
        }
    }

    class TagList {
        private $container: JQuery;
        private items: Array<TagListItem>;

        constructor(private tagger: Tagger) {
            this.items = new Array<TagListItem>();
        }

        public getTagger(): Tagger {
            return this.tagger;
        }

        public addNewItem(label: string, hiddenValue: string): TagListItem {
            var item = new TagListItem(this, label, hiddenValue);

            return this.addItem(item);
        }

        public addItem(item: TagListItem): TagListItem {
            this.items.push(item);

            return item;
        }

        public removeItem(item: TagListItem) {
            var index = this.indexOf(item);
            
            if (index !== -1) {
                this.items.splice(index, 1);
            }

            item.remove(false);
        }

        public indexOf(item: TagListItem): number {
            return $.inArray(item, this.items);
        }

        public render() {
            if (this.$container != null) {
                this.$container.remove();
            }

            this.$container = $("<ul/>")
                .addClass("tag-list")
                .appendTo(this.tagger.getContainer());

            for (var i = 0; i < this.items.length; i++) {
                this.items[i].render();
            }
        }

        public getContainer(): JQuery {
            if (this.$container == null) {
                this.render();
            }

            return this.$container;
        }
    }

    class TagListItem {
        private $container: JQuery;
        private $label: JQuery;
        private $removeButton: JQuery;

        constructor(private tagList: TagList, private label?: string, private hiddenValue?: any) {
        }

        public getLabel(): string {
            return this.label != null ? this.label : (this.hiddenValue != null ? this.hiddenValue.toString() : '');
        }

        public getHiddenValue(): any {
            return this.hiddenValue;
        }

        public render() {
            if (this.$container != null) {
                this.$container.remove();
            }

            this.$container = $("<li/>")
                .addClass("tag-list-item");

            this.$label = $("<span/>")
                .addClass("tag-list-item-label")
                .text(this.getLabel())
                .appendTo(this.$container);

            this.$removeButton = $("<span/>")
                .addClass("tag-list-item-remove")
                .text("X")
                .appendTo(this.$label);

            var fieldName = this.tagList.getTagger().getFieldName();
            var index = this.tagList.indexOf(this);

            if (index < 0) {
                index = 0;
            }

            $("<input/>")
                .attr("type", "hidden")
                .attr("name", fieldName + "[" + index + "].Label")
                .val(this.getLabel())
                .appendTo(this.$container);

            $("<input/>")
                .attr("type", "hidden")
                .attr("name", fieldName + "[" + index + "].HiddenValue")
                .attr("value", this.getHiddenValue())
                .appendTo(this.$container);

            this.tagList.getContainer().append(this.$container);

            this.addEventHandlers();
        }

        public remove(removeFromTagList: boolean = true) {
            if (removeFromTagList) {
                this.tagList.removeItem(this);
            }

            if (this.$container != null) {
                this.$container.remove();
                this.$container = null;
            }
        }

        private addEventHandlers() {
            this.$container.find(".tag-list-item-remove").click(() => {
                this.remove();
            });
        }
    }
}