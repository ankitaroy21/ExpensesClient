import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { EntryService } from '../entry.service';
import { EntryElement } from '../interfaces/EntryElement';
import { UpdateEntryComponent } from '../update-entry/update-entry.component';
 @Component({
  selector: 'app-entries',
  templateUrl: './entries.component.html',
  styleUrls: ['./entries.component.css'],
})
export class EntriesComponent implements OnInit {

  displayedColumns: string[] = ['Description', 'IsExpense', 'Value', 'Actions'];
  dataSource: any;

  id: any;
  route: any;
  pageNo: any;
  pagesize: any;
  sortDir: any;
  total: any;
  constructor(private service: EntryService, private dialog: MatDialog) { }

  ngOnInit(): void {

   this.pagesize=10;
    this.pageNo = 1;
    this.sortDir = true;
    this.service.getAll(this.pageNo, this.pagesize, this.sortDir).subscribe((data: any) => {
      this.total = data.data.length;
      this.dataSource = new MatTableDataSource<EntryElement>(
        data.data as EntryElement[]
      );
    });
  }


  sortFunction(): void {
    this.sortDir = this.sortDir == false ? true : false;
    this.service.getAll(this.pageNo, this.pagesize, this.sortDir).subscribe((data: any) => {
      this.dataSource = new MatTableDataSource<EntryElement>(
        data.data as EntryElement[]
      )
    });
  }

  onPaginate(pageEvent: PageEvent) {
    this.pagesize = +pageEvent.pageSize;
    this.pageNo = +pageEvent.pageIndex + 1;
    this.service.getAll(this.pageNo, this.pagesize, true).subscribe((data: any) => {
      this.dataSource = new MatTableDataSource<EntryElement>(
        data.data as EntryElement[]
      );
    })
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  updateEntry(entry: any) {
    this.dialog.open(UpdateEntryComponent, {
      data: {
        id: entry.id,
        description: entry.description,
        isExpense: entry.isExpense,
        value: entry.value,
      },
    });
  }
}
