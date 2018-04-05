import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { IProduct } from '../../../../model/IProduct';
import { ProductService } from '../../../services/product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  product: IProduct;
  operation: string;

  constructor(private route: ActivatedRoute, private router: Router, private productService: ProductService) { }

  ngOnInit() {
    // load operation parametr from route
    this.operation = this.route.snapshot.params['operation'];

    if (this.operation !== undefined) {
      this.productService.getProduct(this.route.snapshot.params['id'])
                         .subscribe((product: IProduct) => this.product = product);
    } else {
      // TODO: we are creating product
    }
  }

}
