import { TabPanel, useTabs } from 'react-headless-tabs'
import { TabSelector } from './TabSelector'
import ReviewView from './ReviewView'
import { reviews } from './constant'
export function ReviewProduct() {
  const [selectedTab, setSelectedTab] = useTabs(['content1', 'content2', 'content3'])
  return (
    <>
      <nav className='flex border-b border-gray-300  justify-center items-center'>
        <TabSelector isActive={selectedTab === 'content1'} onClick={() => setSelectedTab('content1')}>
          Giá trị học thuật
        </TabSelector>
        <TabSelector isActive={selectedTab === 'content2'} onClick={() => setSelectedTab('content2')}>
          Giá trị kinh tế
        </TabSelector>
        <TabSelector isActive={selectedTab === 'content3'} onClick={() => setSelectedTab('content3')}>
          Giá trị nhân văn
        </TabSelector>
      </nav>
      <div className='p-4'>
        {reviews.map((reviewElement, index) => (
          <TabPanel hidden={selectedTab !== `content${index + 1}`} key={reviewElement.name}>
            <ReviewView
              img={reviewElement.img}
              name={reviewElement.name}
              position={reviewElement.position}
              review={reviewElement.review}
            />
          </TabPanel>
        ))}
      </div>
    </>
  )
}
