import React, { useState } from 'react'
import Table from 'src/components/Table/Table'

export interface ProductDuration {
  productId: string
  durationId: string
  price: number
}

export default function ProductManager() {
  const columnNames = ['Product_Id', 'Duration_Id', 'Price']
  const [data, setData] = useState<ProductDuration[]>([])

  return (
    <div>
      <Table columnNames={columnNames} title='Product Duration Manager' data={data} />
    </div>
  )
}
