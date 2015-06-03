var sagePaging = {
    Paging: function(Total, Rows) {
        this.maxPage = ''
        this.rowPerPage = ''
        this.nav = ''
        this.rowPerPage = Rows;
        this.maxPage = String((Total / this.rowPerPage));
        if (this.maxPage.indexOf('.'))
            this.maxPage = parseInt(this.maxPage) + 1;
    },
    NavPaging: function(pageNumber, pageToShow) {
        pageToShowMinusOne = pageToShow - 1;

        halfPageStart = String(pageToShowMinusOne / 2);
        if (halfPageStart.indexOf('.'))
            halfPageStart = parseInt(halfPageStart);
        halfPageEnd = String(pageToShowMinusOne / 2);
        if (halfPageEnd.indexOf('.'))
            halfPageEnd = parseInt(halfPageEnd) + 1;
        startPage = pageNumber - halfPageStart;
        if (startPage <= 0) startPage = 1;
        endPage = pageNumber + halfPageEnd;
        if ((endPage - startPage) != pageToShowMinusOne) {
            endPage = startPage + pageToShowMinusOne;
        }
        if (endPage > this.maxPage) {
            startPage = this.maxPage - pageToShowMinusOne;
            endPage = this.maxPage
        }
        if (startPage <= 0) {
            startPage = 1
        }
        if (pageNumber > 1) {
            page = parseInt(pageNumber) - 1;
            prev = '<a href="#" alt="' + page + '" class="cssClassPagePrevNxt">&lsaquo;</a>';
            first = '<a href="#" alt="1" class="cssClassPagePrevNxt">&laquo;</a>';
        }
        else {
            prev = '';
            first = '';
        }
        for (var i = startPage; i < endPage; i++) {
            if (pageNumber == i) {
                this.nav += '<span>' + i + '</span>';
            } else {
                this.nav += '<a href="#" alt="' + i + '">' + i + '</a>';
            }
        }
        if (pageNumber < this.maxPage) {
            page = parseInt(pageNumber) + 1;
            next = '<a href="#" alt="' + page + '" class="cssClassPagePrevNxt">&rsaquo;</a>';
            last = '<a href="#" alt="' + this.maxPage + '" class="cssClassPagePrevNxt">&raquo;</a>';
        }
        else {
            next = '';
            last = '';
        }
        this.nav = first + prev + this.nav + next + last;
    }
}