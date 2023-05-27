import { TabPanel, useTabs } from 'react-headless-tabs'
import { TabSelector } from './TabSelector'

export function TabPanelCustom() {
  const [selectedTab, setSelectedTab] = useTabs(['Text', 'Json'])

  return (
    <>
      <nav className='flex border-b border-gray-300'>
        <TabSelector isActive={selectedTab === 'Text'} onClick={() => setSelectedTab('Text')}>
          Description
        </TabSelector>
      </nav>
      <div className='p-4'>
        <TabPanel hidden={selectedTab !== 'Text'}>Text</TabPanel>
        <TabPanel hidden={selectedTab !== 'Json'}>Json view</TabPanel>
      </div>
    </>
  )
}
